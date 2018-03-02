using DataAccess.BLL;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using Newtonsoft.Json;
using Service.Contracts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Web.Api.Common;

namespace Web.Api.Providers
{
    public class SimpleRefreshTokenProvider : IAuthenticationTokenProvider
    {

        private readonly IUserService _systemUserService;
        private readonly IUserLoginService _userLoginService;
        public SimpleRefreshTokenProvider()
        {
            _systemUserService = StructureMap.ObjectFactory.GetInstance<IUserService>();
            _userLoginService = StructureMap.ObjectFactory.GetInstance<IUserLoginService>();

        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            try
            {
                var clientid = context.Ticket.Properties.Dictionary["as:client_id"];
                var userId = context.Ticket.Properties.Dictionary["UserId"];
                var ipAddress = context.Ticket.Properties.Dictionary["IPAddress"];

                if (string.IsNullOrEmpty(clientid))
                {
                    return;
                }

                var refreshTokenId = Guid.NewGuid().ToString("n");
                var userService = StructureMap.ObjectFactory.GetInstance<IUserService>();
                User user = userService.GetUsers(x => x.Id == int.Parse(userId)).FirstOrDefault();


                UserLogin tbluserLogin = new UserLogin
                {
                    UserId = int.Parse(userId),
                    IPAddress = ipAddress,
                    Successful = true,
                    TokenId = refreshTokenId,
                    AppId = clientid,
                    DateUtc = DateTime.UtcNow,
                    IsLoggedIn = true

                };

                context.Ticket.Properties.IssuedUtc = tbluserLogin.DateUtc;
                tbluserLogin.ProtectedTicket = context.SerializeTicket();
                _userLoginService.InsertUserLogin(tbluserLogin);

                context.SetToken(refreshTokenId);
            }
            catch
            { 
            
            }

            

        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            try
            {
                IUserLoginService _userLoginService = StructureMap.ObjectFactory.GetInstance<IUserLoginService>();
                var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

                string hashedTokenId = CommonUtility.GetHash(context.Token);


                var refreshToken = _userLoginService.GetUserLogins(x => x.TokenId == hashedTokenId).FirstOrDefault();
            }
            catch
            { 
            
            }

            

        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }
    }
}