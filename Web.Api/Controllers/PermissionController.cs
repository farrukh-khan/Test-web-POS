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
using System.Web.Http;
using Web.Api.Models.Permission;

namespace Web.Api.Controllers
{
    public class PermissionController : ApiController
    {
        #region Fields
        private readonly ISpService _spService;
        private readonly IPermissionService _permissionService;
        private readonly IRolePermissionService _rolePermissionService;

        private readonly IActionCategoryService _actionCategoryService;
        private readonly IActionService _actionService;

        #endregion

        #region Ctor
        public PermissionController()
        {
            _spService = StructureMap.ObjectFactory.GetInstance<ISpService>();
            _permissionService = StructureMap.ObjectFactory.GetInstance<IPermissionService>();
            _rolePermissionService = StructureMap.ObjectFactory.GetInstance<IRolePermissionService>();
            _actionCategoryService = StructureMap.ObjectFactory.GetInstance<IActionCategoryService>();
            _actionService = StructureMap.ObjectFactory.GetInstance<IActionService>();
        }
        #endregion

        #region Api Controllers



        /// <summary>
        /// Permission section start
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Authorize]
        [ActionName("GetPermissions")]

        public IHttpActionResult GetPermissions(long roleId = 1)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] param = {
                                        new SqlParameter("@roleId",roleId),                                         
                                       };
                DataSet data = _spService.ExcuteSpAnonmious("prc_getPermission", param, 2);

                var roles = data.Tables[0].AsEnumerable().Select(s => new Role
                    {
                        Id = s.Field<long>("Id"),
                        RoleName = s.Field<string>("RoleName")
                    }).ToList();

                var permission = data.Tables[1].AsEnumerable().GroupBy(g => g.Field<string>("Category")).Select(s => new CategoryPermissionModel
                {
                    Category = s.First().Field<string>("Category"),
                    Role = s.First().Field<string>("Role"),
                    RoleId = s.First().Field<long>("RoleId"),
                    Actions = s.GroupBy(gg => gg.Field<string>("Action")).Select(ss => new ActionPermissionModel
                    {
                        ActionId = ss.First().Field<long>("ActionId"),
                        Action = ss.First().Field<string>("Action"),
                        PermissionModel = ss.Select(sss => new PermissionModel
                        {
                            PermissionId = sss.Field<long>("PermissionId"),
                            Permission = sss.Field<string>("Permission"),
                            IsAllowed = sss.Field<bool>("IsAllowed"),
                            RolePermissionId = sss.Field<long>("RolePermissionId")
                        }).ToList(),

                    }).ToList(),

                }).ToList();


                return Ok(new ArrayList() { roles, permission });

            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }



        [HttpGet]
        [Authorize]
        [ActionName("GetPermissionByRoleId")]

        public IHttpActionResult GetPermissionByRoleId(long id)
        {
            try
            {
                DataTable dt = new DataTable();
                string proc = "prc_ReportExecute";
                string query = "select p.PermName from permission as p inner join rolepermission as rp on p.Id = rp.PermId";
                query += " inner join [Role] as r on r.Id = rp.roleid where r.id = " + id;

                SqlParameter[] param = {
                                        new SqlParameter("@CurrentPage",1), 
                                        new SqlParameter("@PageSize",-1),
                                        new SqlParameter("@Query",query),                                        
                                       };

                string[] data = _spService.ExcuteSpAnonmious(proc, param, 1).Tables[0].AsEnumerable().Select(s => s.Field<string>("PermName")).ToArray();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }


        [HttpPost]
        [Authorize]
        [ActionName("PermissionSubmit")]

        public IHttpActionResult PermissionSubmit(IEnumerable<CategoryPermissionModel> model)
        {
            try
            {

                string query = "";

                foreach (var item in model)
                {

                    foreach (var action in item.Actions)
                    {
                        foreach (var perm in action.PermissionModel)
                        {

                            query += string.Format("update RolePermission set IsAllowed = '{0}' where id ={1};", perm.IsAllowed, perm.RolePermissionId);
                        }
                    }
                }

                SqlParameter[] param = {

                                       };

                int retVal = _spService.ExcuteSp(query, param);

                return Ok(true);

            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }



        [HttpGet]
        [ActionName("GetAssginedPermission")]
        public IHttpActionResult GetAssginedPermission(string url, long? roleId, long userType)
        {

            PermissionAccess permAccess = new PermissionAccess();

            try
            {

                if (userType == 1 && (url.ToUpper() == "COMPANIES" || url.ToUpper() == "COMPANY" || url.ToUpper() == "EDITCOMPANY"

                    || url.ToUpper() == "COMPANYROLE"

                    || url.ToUpper() == "COMPANYUSER"

                    || url.ToUpper() == "EDITCOMPANY"
                    
                    ))
                {
                    permAccess.View = true;
                    permAccess.Add = true;
                    permAccess.Edit = true;
                    permAccess.Delete = true;
                    permAccess.IsAllowed = true;
                    return Ok(permAccess);
                }
                
                else
                {
                    DataTable dt = new DataTable();

                    bool retVal = false;
                    if (url.ToUpper().Equals("SKIP"))
                    {
                        retVal = true;
                        permAccess.IsAllowed = true;
                    }
                    else if (url.Contains(","))
                    {

                        long reportId = long.Parse(url.Split(',')[1]);

                        SqlParameter[] param = {
                                        new SqlParameter("@roleId",roleId), 
                                        new SqlParameter("@reportId",reportId),                                        
                                       };
                        DataSet data = _spService.ExcuteSpAnonmious("prc_getReportPermission", param, 1);
                        var isAllowed = data.Tables[0].AsEnumerable().Select(s => new
                        {
                            IsAllowed = s.Field<bool>("IsAllowed"),
                        }).FirstOrDefault();


                        if (isAllowed != null)
                        {
                            permAccess.IsAllowed = isAllowed.IsAllowed;
                        }

                    }
                    else
                    {

                        SqlParameter[] param = {
                                        new SqlParameter("@pageName",url),                                         
                                        new SqlParameter("@roleId",roleId),                                         
                                       };
                        DataSet data = _spService.ExcuteSpAnonmious("prc_getAssginedPermission", param, 2);
                        var permission = data.Tables[0].AsEnumerable().Select(s => new
                        {
                            IsAllowed = s.Field<bool>("IsAllowed"),
                            RoleName = s.Field<string>("RoleName"),
                            PermName = s.Field<string>("PermName"),
                            PageName = s.Field<string>("PageName"),
                        }).FirstOrDefault();



                        var access = data.Tables[1].AsEnumerable().Select(s => new
                        {
                            IsAllowed = s.Field<bool>("IsAllowed"),
                            PermName = s.Field<string>("PermName"),
                        });



                        if (access != null)
                        {

                            foreach (var item in access)
                            {
                                switch (item.PermName.ToUpper())
                                {
                                    case "VIEW":
                                        permAccess.View = item.IsAllowed;
                                        break;
                                    case "ADD":
                                        permAccess.Add = item.IsAllowed;
                                        break;
                                    case "EDIT":
                                        permAccess.Edit = item.IsAllowed;
                                        break;
                                    case "DELETE":
                                        permAccess.Delete = item.IsAllowed;
                                        break;
                                }

                                permAccess.IsAllowed = permission.IsAllowed;
                            }
                        }



                    }
                }


                return Ok(permAccess);
            }
            catch
            {
                return Ok(permAccess);
            }
        }


        #endregion
    }
}
