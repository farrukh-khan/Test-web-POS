using DataAccess.BLL;
using Service.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web.Http;
using Web.Api.Common;
using Web.Api.Models.Company;
using Web.Api.Models.Permission;
using Web.Api.Models.User;

namespace Web.Api.Controllers
{
    public class CompanyController : ApiController
    {

        #region Fields
        private readonly ICompanyService _companyService;
        private readonly ISpService _spService;
        private readonly ISystemSettingService _systemSettingService;
        private readonly IUserService _userService;
        private readonly IRoleService _RoleService;
        private readonly IRolePermissionService _rolePermissionService;



        #endregion

        #region Ctor
        public CompanyController()
        {
            _companyService = StructureMap.ObjectFactory.GetInstance<ICompanyService>();
            _spService = StructureMap.ObjectFactory.GetInstance<ISpService>();
            _systemSettingService = StructureMap.ObjectFactory.GetInstance<ISystemSettingService>();
            _userService = StructureMap.ObjectFactory.GetInstance<IUserService>();
            _RoleService = StructureMap.ObjectFactory.GetInstance<IRoleService>();
            _rolePermissionService = StructureMap.ObjectFactory.GetInstance<IRolePermissionService>();
        }
        #endregion

        #region Api Controllers



        /// <summary>
        /// Company section start
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Authorize]
        [ActionName("GetCompanys")]

        public IHttpActionResult GetCompanys()
        {
            try
            {
                SqlParameter[] param = {                                        
                                       };

                DataSet data = _spService.ExcuteSpAnonmious("prc_getCompanies", param, 1);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }




        /// <summary>
        /// Company section start
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Authorize]
        [ActionName("GetCustomerData")]

        public IHttpActionResult GetCustomerData()
        {
            try
            {
                SqlParameter[] param = {                                        
                                       };

                DataSet data = _spService.ExcuteSpAnonmious("prc_getCustomerData", param, 3);

                var permission = data.Tables[2].AsEnumerable().GroupBy(g => g.Field<string>("Category")).Select(s => new CategoryPermissionModel
                {
                    Category = s.First().Field<string>("Category"),
                    Role = s.First().Field<string>("Role"),
                    RoleId = s.First().Field<int>("RoleId"),
                    Actions = s.GroupBy(gg => gg.Field<string>("Action")).Select(ss => new ActionPermissionModel
                    {
                        ActionId = ss.First().Field<int>("ActionId"),
                        Action = ss.First().Field<string>("Action"),
                        PermissionModel = ss.Select(sss => new PermissionModel
                        {
                            PermissionId = sss.Field<int>("PermissionId"),
                            Permission = sss.Field<string>("Permission"),
                            IsAllowed = sss.Field<bool>("IsAllowed"),
                            RolePermissionId = sss.Field<int>("RolePermissionId")
                        }).ToList(),

                    }).ToList(),

                }).ToList();

                return Ok(new ArrayList() { data.Tables[0], data.Tables[1], permission });


            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }



        [HttpGet]
        [Authorize]
        [ActionName("GetCompanyById")]

        public IHttpActionResult GetCompanyById(int id)
        {
            try
            {

                SqlParameter[] param = {         
                                           new SqlParameter("@companyId",id)
                                       };

                DataSet data = _spService.ExcuteSpAnonmious("prc_getCompanyById", param, 5);

                return Ok(data);

            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        [Authorize]
        [ActionName("UpdateCompanySubmit")]

        public IHttpActionResult UpdateCompanySubmit(CompanyModel model)
        {
            try
            {

                var data = _companyService.GetCompanys(x => x.Name.Trim() == model.Name.Trim()).ToList();

                if (data.Count > 1)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Company name already exist."));
                }

                var company = data.FirstOrDefault();

                if (company != null)
                {
                    company.Name = model.Name;
                    company.CountryId = model.CountryId;
                    company.IsActive = model.IsActive;
                    _companyService.UpdateCompany(company);
                }


                return Ok(true);
            }

            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }


        }










        [HttpPost]
        [Authorize]
        [ActionName("CompanySubmit")]

        public IHttpActionResult CompanySubmit(CompanyModel model)
        {
            try
            {


                var data = _companyService.GetCompanys(x => x.Name.Trim() == model.Name.Trim()).ToList();


                if (data.Any())
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Company name already exist."));
                }


                if (string.IsNullOrEmpty(model.Rolename))
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Please provide role name."));
                }


                if (string.IsNullOrEmpty(model.User.Email))
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Please provide Email Address."));
                }




                Company company = new Company()
                {
                    Name = model.Name,
                    Address1 = model.Address1,
                    Address2 = model.Address2,
                    Phone1 = model.Phone1,
                    Phone2 = model.Phone2,                    
                    CountryId = model.CountryId,
                    IsActive = model.IsActive
                };

                _companyService.InsertCompany(company);

                Role role = new Role
                {
                    RoleName = model.Rolename,
                    CompanyId = company.Id
                };

                _RoleService.InsertRole(role);

                var user = AutoMapper.Mapper.Map<UserModel, User>(model.User);

                user.Password = PasswordHasher.CreateTextString(PasswordHasher.HashPassword(user.Password, SHA256Managed.Create()));
                user.IsActive = true;
                user.Code = 0000;
                user.IsCodeUsed = true;
                user.CodeExpiry = DateTime.Now;
                

                user.RoleId = role.Id;

                user.CompanyId = company.Id;
                
                _userService.InsertUser(user);
                //if (model.User.UserType == 1)
                //{
                //    SystemSetting settings = new SystemSetting()
                //    {
                //        AnswerTime = 10,
                //        LocalCode = 234
                //    };
                //    _systemSettingService.InsertSystemSetting(settings);
                //}



                foreach (var item in model.Permission)
                {

                    foreach (var action in item.Actions)
                    {
                        foreach (var perm in action.PermissionModel)
                        {

                            RolePermission rp = new RolePermission()
                            {

                                ActionId = action.ActionId,
                                RoleId = role.Id,
                                PermId = perm.PermissionId,
                                IsAllowed = perm.IsAllowed


                            };

                            _rolePermissionService.InsertRolePermission(rp);

                        }
                    }
                }





                return Ok(true);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }







        [HttpPost]
        [Authorize]
        [ActionName("RolePermissionSubmit")]

        public IHttpActionResult RolePermissionSubmit(RolePermissionModel model)
        {
            try
            {


                if (model.Id == 0)
                {
                    Role role = new Role
                    {
                        RoleName = model.RoleName,
                        CompanyId = model.CompanyId
                    };

                    _RoleService.InsertRole(role);


                    foreach (var item in model.PermissionList)
                    {

                        foreach (var action in item.Actions)
                        {
                            foreach (var perm in action.PermissionModel)
                            {

                                RolePermission rp = new RolePermission()
                                {

                                    ActionId = action.ActionId,
                                    RoleId = role.Id,
                                    PermId = perm.PermissionId,
                                    IsAllowed = perm.IsAllowed


                                };

                                _rolePermissionService.InsertRolePermission(rp);

                            }
                        }
                    }
                }
                else
                {
                    var role = _RoleService.GetRoles(x => x.Id == model.Id && x.CompanyId == model.CompanyId).FirstOrDefault();

                    if (role != null)
                    {

                        role.RoleName = model.RoleName;
                        _RoleService.UpdateRole(role);


                        foreach (var item in model.PermissionList)
                        {

                            foreach (var action in item.Actions)
                            {
                                foreach (var perm in action.PermissionModel)
                                {

                                    var rolePerm = _rolePermissionService.GetRolePermissions(x => x.Id == perm.RolePermissionId).FirstOrDefault();
                                    if (rolePerm != null)
                                    {
                                        rolePerm.ActionId = action.ActionId;
                                        rolePerm.RoleId = role.Id;
                                        rolePerm.PermId = perm.PermissionId;
                                        rolePerm.IsAllowed = perm.IsAllowed;
                                        _rolePermissionService.UpdateRolePermission(rolePerm);
                                    }




                                }
                            }
                        }

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
        [ActionName("DeleteCompany")]
        public IHttpActionResult DeleteCompany(int id)
        {
            try
            {
                SqlParameter[] param = {         
                                           new SqlParameter("@companyId",id)
                                       };

                DataSet data = _spService.ExcuteSpAnonmious("prc_deleteCompany", param, 1);

                return Ok(data.Tables[0]);

            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }


        [HttpGet]
        [Authorize]
        [ActionName("GetCountries")]

        public IHttpActionResult GetCountries()
        {
            try
            {
                SqlParameter[] param = {                                        
                                       };

                DataSet data = _spService.ExcuteSpAnonmious("prc_getCountries", param, 1);

                return Ok(data.Tables[0]);


            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }


        #endregion
    }
}
