using DataAccess.BLL;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using Web.Api.Common;
namespace Web.Api.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {

        private readonly IAppService _appService;
        private readonly ICompanyService _companyService;
        private readonly IUserLoginService _userLoginService;
        private readonly IPatientService _customerService;
        private readonly IRoleService _roleService;

        public SimpleAuthorizationServerProvider()
        {

            _appService = StructureMap.ObjectFactory.GetInstance<IAppService>();
            _companyService = StructureMap.ObjectFactory.GetInstance<ICompanyService>();
            _userLoginService = StructureMap.ObjectFactory.GetInstance<IUserLoginService>();
            _customerService = StructureMap.ObjectFactory.GetInstance<IPatientService>();
            _roleService = StructureMap.ObjectFactory.GetInstance<IRoleService>();

        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {


            try
            {
                string clientId = string.Empty;
                string clientSecret = string.Empty;
                App client = null;

                if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
                {
                    context.TryGetFormCredentials(out clientId, out clientSecret);
                }

                if (context.ClientId == null)
                {
                    //Remove the comments from the below line context.SetError, and invalidate context 
                    //if you want to force sending clientId/secrects once obtain access tokens. 
                    context.Validated();
                    //context.SetError("invalid_clientId", "ClientId should be sent.");
                    return Task.FromResult<object>(null);
                }

                client = _appService.GetApps(x => x.Id == context.ClientId).FirstOrDefault();
                if (client == null)
                {
                    context.SetError("invalid_clientId", string.Format("Client '{0}' is not registered in the system.", context.ClientId));
                    return Task.FromResult<object>(null);
                }

                if (client.ApplicationType == (int)Web.Api.Models.ApplicationTypes.NativeConfidential)
                {
                    if (string.IsNullOrWhiteSpace(clientSecret))
                    {
                        context.SetError("invalid_clientId", "Client secret should be sent.");
                        return Task.FromResult<object>(null);
                    }
                    else
                    {
                        if (client.Secret != Web.Api.Common.CommonUtility.GetHash(clientSecret))
                        {
                            context.SetError("invalid_clientId", "Client secret is invalid.");
                            return Task.FromResult<object>(null);
                        }
                    }
                }

                if (!client.Active)
                {
                    context.SetError("invalid_clientId", "Client is inactive.");
                    return Task.FromResult<object>(null);
                }

                context.OwinContext.Set<string>("as:clientAllowedOrigin", client.AllowedOrigin);
                context.OwinContext.Set<string>("as:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString());

                context.Validated();
                return Task.FromResult<object>(null);
            }
            catch
            {
                return Task.FromResult<object>(null);
            }


        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            try
            {


                var scop = context.Scope[0].Split(',');

                string ip = HttpContext.Current.Request.UserHostAddress;
                bool isLogOffUser = bool.Parse(scop[0]);
                string token = scop[1];
                var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");

                if (allowedOrigin == null) allowedOrigin = "*";

                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });
                IUserService userService = StructureMap.ObjectFactory.GetInstance<IUserService>();
                User user = userService.GetUsers(x => x.Email == context.UserName).FirstOrDefault();


                if (user == null)
                {
                    CommonUtility.LoginFailedLog(_userLoginService, ip, context.ClientId);
                    context.SetError("invalid_grant", "Invalid username and password.");
                    return;
                }

                if (!user.IsActive)
                {
                    CommonUtility.LoginFailedLog(_userLoginService, ip, context.ClientId, user.Id);
                    context.SetError("invalid_grant", "Invalid username and password.");
                    return;
                }

                if (user.IsLocked)
                {
                    CommonUtility.LoginFailedLog(_userLoginService, ip, context.ClientId, user.Id);
                    context.SetError("invalid_grant", "Your account is locked. Please contact system administrator.");
                    return;
                }

                if (user.IsSuspended)
                {
                    CommonUtility.LoginFailedLog(_userLoginService, ip, context.ClientId, user.Id);
                    context.SetError("invalid_grant", "Your account is suspended. Please contact system administrator.");
                    return;
                }


                if (user.RoleId != 1)
                {
                    var company = _companyService.GetCompanyById(user.CompanyId);

                    if (!company.IsActive)
                    {
                        CommonUtility.LoginFailedLog(_userLoginService, ip, context.ClientId, user.Id);
                        context.SetError("invalid_grant", "Invalid username and password.");
                        return;
                    }

                }

                bool isValid = false;

                if (scop[2].ToUpper() == "ADMINLOGIN")
                {
                    isValid = user.Password == context.Password ? true : false;

                }
                else
                {
                    isValid = PasswordHasher.ComparePassword(context.Password, PasswordHasher.CreateByteArray(user.Password), SHA256Managed.Create());
                }


                if (!isValid)
                {
                    user.InvalidPasswordAttempt = user.InvalidPasswordAttempt + 1;
                    StructureMap.ObjectFactory.GetInstance<IUserService>().UpdateUser(user);
                    CommonUtility.LoginFailedLog(_userLoginService, ip, context.ClientId, user.Id);
                    context.SetError("invalid_grant", "Invalid username and password.");
                    return;
                }
                var role = _roleService.GetRoleById(user.RoleId ?? 0);

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("sub", context.UserName));
                identity.AddClaim(new Claim("UserId", user.Id.ToString()));
                identity.AddClaim(new Claim("IPAddress", ip));
                identity.AddClaim(new Claim("FirstName", user.FirstName));
                identity.AddClaim(new Claim("LastName", user.LastName));

                identity.AddClaim(new Claim("UserType", role.UserType.ToString()));
                identity.AddClaim(new Claim("RoleId", user.RoleId.ToString()));
                identity.AddClaim(new Claim("ClientId", user.CompanyId.ToString()));
                identity.AddClaim(new Claim("LabId", user.LabId.ToString()));


                var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId
                    },
                    {
                        "userName", context.UserName
                    },
                    {
                        "UserId",  user.Id.ToString()
                    },
                     {
                        "IPAddress",  ip
                    }
                    ,
                     {
                        "FirstName",  user.FirstName
                    }
                    ,
                     {
                        "LastName", user.LastName
                    }


                    ,
                     {
                        "UserType", role.UserType.ToString()
                    }
                    ,
                     {
                        "RoleId", user.RoleId.ToString()
                    },

                     {
                        "CompanyId", user.CompanyId.ToString()
                    }
                    ,
                     {
                        "LabId", user.LabId.ToString()
                    }



                });

                var ticket = new AuthenticationTicket(identity, props);
                context.Validated(ticket);
            }
            catch (Exception ex)
            {
                context.SetError("invalid_grant", ex.ToString());
            }

        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            try
            {
                var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
                var currentClient = context.ClientId;

                if (originalClient != currentClient)
                {
                    context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
                    return Task.FromResult<object>(null);
                }

                // Change auth ticket for refresh token requests
                var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
                newIdentity.AddClaim(new Claim("newClaim", "newValue"));

                var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
                context.Validated(newTicket);

                return Task.FromResult<object>(null);
            }
            catch
            {
                return Task.FromResult<object>(null);
            }

        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            try
            {
                foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
                {
                    context.AdditionalResponseParameters.Add(property.Key, property.Value);
                }

                return Task.FromResult<object>(null);
            }
            catch
            {
                return Task.FromResult<object>(null);

            }


        }

    }
}