using DataAccess.BLL;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web.Http;
using Web.Api.Common;
using Web.Api.Models.User;

namespace Web.Api.Controllers
{
    public class AccountController : ApiController
    {
        #region Fields
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        private readonly IUserLoginService _userLoginService;
        private readonly IRolePermissionService _rolePermissionService;
        private readonly ISystemSettingService _systemSettingervice;

        private readonly ICompanyService _companyService;


        #endregion

        #region Ctor
        public AccountController()
        {

            _userService = StructureMap.ObjectFactory.GetInstance<IUserService>();
            _roleService = StructureMap.ObjectFactory.GetInstance<IRoleService>();
            _rolePermissionService = StructureMap.ObjectFactory.GetInstance<IRolePermissionService>();
            _userLoginService = StructureMap.ObjectFactory.GetInstance<IUserLoginService>();
            _systemSettingervice = StructureMap.ObjectFactory.GetInstance<ISystemSettingService>();
            _companyService = StructureMap.ObjectFactory.GetInstance<ICompanyService>();



        }
        #endregion

        #region Api Controllers



        /// <summary>
        /// User section start
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Authorize]
        [ActionName("GetUsers")]

        public IHttpActionResult GetUsers(int CompanyId)
        {
            try
            {
                return Ok(GetUsersAll(CompanyId));
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        private IEnumerable<UserModel> GetUsersAll(int companyId)
        {


            var users = _userService.GetUsers(x => x.CompanyId == companyId && x.IsActive == true).Select(s => new UserModel
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                RoleId = s.RoleId,
                RoleName = s.RoleId != null ? _roleService.GetRoleById(s.RoleId ?? 0).RoleName : "",
                IsActive = s.IsActive
            }).ToList();
            return users;
        }

        [HttpGet]
        [Authorize]
        [ActionName("GetUserById")]

        public IHttpActionResult GetUserById(int id)
        {
            try
            {
                var user = _userService.GetUsers(x => x.Id == id).FirstOrDefault();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }


        [HttpPost]
        [Authorize]
        [ActionName("UserSubmit")]

        public IHttpActionResult UserSubmit(UserModel model)
        {
            try
            {

                var users = _userService.GetUsers(x => x.Email.Trim() == model.Email.Trim()).ToList();


                if (model.Id <= 0)
                {
                    if (users.Any())
                    {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Email already exist."));
                    }


                    if (model.Password != "                    " && model.ConfirmPassword != "                    " && model.Password == model.ConfirmPassword)
                    {
                        model.Password = PasswordHasher.CreateTextString(PasswordHasher.HashPassword(model.Password, SHA256Managed.Create()));
                    }


                    var company = _companyService.GetCompanyById(model.CompanyId);

                    var user = AutoMapper.Mapper.Map<UserModel, User>(model);
                    user.IsActive = true;
                    user.Code = 0000;
                    user.IsCodeUsed = true;
                    user.CodeExpiry = DateTime.Now;

                    _userService.InsertUser(user);
                    //if (model.UserType == 1)
                    //{
                    //    SystemSetting settings = new SystemSetting()
                    //    {
                    //        AnswerTime = 10,
                    //        LocalCode = 234
                    //    };
                    //    _systemSettingervice.InsertSystemSetting(settings);
                    //}

                }
                else
                {
                    if (users.Count() > 1)
                    {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Email already exist."));
                    }


                    var user = _userService.GetUserById(model.Id);


                    if (model.Password != "                    " && model.ConfirmPassword != "                    " && model.Password == model.ConfirmPassword)
                    {
                        model.Password = PasswordHasher.CreateTextString(PasswordHasher.HashPassword(model.Password, SHA256Managed.Create()));
                    }
                    else
                    {
                        model.Password = user.Password;
                    }


                    if (user != null)
                    {
                        user.Email = model.Email;
                        user.Password = model.Password;
                        user.FirstName = model.FirstName;
                        user.LastName = model.LastName;
                        user.RoleId = model.RoleId;
                        user.IsActive = model.IsActive;

                        _userService.UpdateUser(user);
                    }
                    else
                    {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Role Id does not exist, please try again!"));
                    }


                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }




        [HttpGet]
        [Authorize]
        [ActionName("DeleteUser")]
        public IHttpActionResult DeleteUser(int id, int CompanyId)
        {
            try
            {
                var users = _userService.GetUserById(id);

                if (users == null)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "User does not exist."));
                }

                users.IsActive = false;
                _userService.UpdateUser(users);


                return Ok(GetUsersAll(CompanyId));
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }



        /// Password reset 

        [HttpGet]
        [ActionName("EmailVarification")]
        public IHttpActionResult EmailVarification(string email)
        {
            try
            {

                var user = _userService.GetUsers(x => x.Email == email && x.IsActive).FirstOrDefault();

                if (user != null)
                {

                    Random rnd = new Random();
                    int code = rnd.Next(1000, 9999);
                    user.Code = code;
                    user.IsCodeUsed = false;
                    user.CodeExpiry = DateTime.Now.AddMinutes(5);
                    string strBody = "<HTML><BODY><P><font color='#4e0145' face='verdana' size='2'>Hi " + user.LastName + " " + user.FirstName + ",<BR /> <BR />";
                    strBody = strBody + "You've requested to reset your password, please find it below:<BR /><BR /> ";
                    strBody = strBody + "your verification code: " + code + " <BR /><BR />";
                    strBody = strBody + "***Please do not reply to this system generated email.<BR /><BR />";
                    strBody = strBody + "Regards,<BR />Smart Reports</font></P></BODY></HTML>";

                    _userService.UpdateUser(user);
                    MailManager.SendVerificationCode(user, strBody);
                }
                else
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Email does not exist."));
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }


        [HttpPost]
        [ActionName("VerifyCode")]
        public IHttpActionResult VerifyCode(PasswordResetModel model)
        {
            try
            {
                var user = _userService.GetUsers(x => x.Email == model.Email).FirstOrDefault();
                if (user == null)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Email does not exist."));
                }
                else if (user != null && !user.Code.Equals(model.Code) && user.CodeExpiry > DateTime.Now && user.IsCodeUsed == false)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Invalid verification code."));

                }

                user.IsCodeUsed = true;
                user.CodeExpiry = DateTime.Now;
                _userService.UpdateUser(user);
                return Ok();
            }
            catch (Exception ex)
            {

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }


        }

        [HttpPost]
        [ActionName("ResetPassword")]
        public IHttpActionResult ResetPassword(PasswordResetModel model)
        {
            try
            {
                string pass = "";
                var user = _userService.GetUsers(x => x.Email == model.Email).FirstOrDefault();

                if (user == null)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Email does not exist."));
                }


                pass = user.Password;

                if (model.Password != model.ConfirmPassword)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Password and confirm password does not match."));
                }

                user.Password = PasswordHasher.CreateTextString(PasswordHasher.HashPassword(model.Password, SHA256Managed.Create()));
                _userService.UpdateUser(user);

                return Ok();

            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }


        }

        //// Get api/Account/Logout
        [HttpGet]
        [ActionName("Logout")]
        public IHttpActionResult Logout(string email, string token)
        {
            try
            {
                var user = _userService.GetUsers(x => x.Email == email).FirstOrDefault();
                if (user != null)
                {
                    var userLogin = _userLoginService.GetUserLogins(x => x.UserId == user.Id && x.TokenId == token && x.IsLoggedIn).ToList();

                    foreach (var data in userLogin)
                    {
                        data.IsLoggedIn = false;
                        _userLoginService.UpdateUserLogin(data);
                    }
                }
                else
                {

                    if (!string.IsNullOrEmpty(token))
                    {
                        var userLogin = _userLoginService.GetUserLogins(x => x.TokenId == token && x.IsLoggedIn).ToList();
                        foreach (var data in userLogin)
                        {
                            data.IsLoggedIn = false;
                            _userLoginService.UpdateUserLogin(data);
                        }
                    }
                }

                return Ok("Successfully logout");
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }

        }





        /// <summary>
        /// User section end
        /// </summary>
        /// <returns></returns>




        /// <summary>
        /// Role section start
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Authorize]
        [ActionName("GetRoles")]

        public IHttpActionResult GetRoles(int? CompanyId)
        {
            try
            {
                var roles = _roleService.GetRoles(x => x.CompanyId == CompanyId && x.UserType != 1).Select(s=> new {
                    s.RoleName,
                    s.Id
                }).ToList();
                return Ok(roles);

            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpGet]
        [Authorize]
        [ActionName("GetRoleById")]

        public IHttpActionResult GetRoleById(int id, int? CompanyId)
        {
            try
            {
                var role = _roleService.GetRoles(x => x.Id == id && x.CompanyId == CompanyId && x.UserType != 1).FirstOrDefault();
                return Ok(role);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }


        [HttpPost]
        [Authorize]
        [ActionName("RoleSubmit")]

        public IHttpActionResult RoleSubmit(Role model)
        {
            try
            {

                var roles = _roleService.GetRoles(x => x.RoleName.Trim() == model.RoleName.Trim() && x.CompanyId == model.CompanyId).ToList();
                //                INSERT INTO rolepermission (RoleId,PermId, ActionId, IsAllowed)
                //SELECT
                //  5,PermId, ActionId, IsAllowed
                //FROM
                //  rolepermission
                //WHERE
                //  RoleId = 3




                if (model.Id <= 0)
                {
                    if (roles.Any())
                    {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Role name already exist."));
                    }
                    model.UserType = 3;
                    _roleService.InsertRole(model);
                }
                else
                {
                    if (roles.Count() > 1)
                    {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Role name already exist."));
                    }


                    var role = _roleService.GetRoleById(model.Id);

                    if (role != null)
                    {
                        role.RoleName = model.RoleName;
                        role.UserType = 3;
                        _roleService.UpdateRole(role);
                    }
                    else
                    {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Role Id does not exist, please try again!"));
                    }


                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }




        [HttpGet]
        [Authorize]
        [ActionName("DeleteRole")]
        public IHttpActionResult DeleteRole(int id, int? CompanyId)
        {
            try
            {
                var roles = _roleService.GetRoles(x => x.Id == id && x.CompanyId == CompanyId).FirstOrDefault();

                if (roles == null)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Role does not exist."));
                }


                var user = _userService.GetUsers(x => x.RoleId == id && x.CompanyId == CompanyId);

                if (user.Any())
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Sorry role can not be deleted, Role assigned to user."));

                }

                var rolePermissions = _rolePermissionService.GetRolePermissions(x => x.RoleId == id).ToList();

                foreach (var item in rolePermissions)
                {
                    _rolePermissionService.DeleteRolePermission(item);

                }

                _roleService.DeleteRole(roles);
                var roleList = _roleService.GetRoles(x => x.CompanyId == CompanyId).ToList();
                return Ok(roleList);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        /// <summary>
        /// Role section end
        /// </summary>
        /// <returns></returns>

        #endregion
    }

}
