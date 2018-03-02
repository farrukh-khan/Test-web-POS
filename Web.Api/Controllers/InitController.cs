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
using Web.Api.Models.Dashboard;
using Web.Api.Models.Permission;
namespace Web.Api.Controllers
{
    public class InitController : ApiController
    {
        #region Fields
        private readonly ISpService _spService;

        #endregion

        #region Ctor
        public InitController()
        {
            _spService = StructureMap.ObjectFactory.GetInstance<ISpService>();
        }
        #endregion

        #region Api Controllers



        /// <summary>
        /// Division section start
        /// </summary>
        /// <returns></returns>


        [HttpGet]
        [ActionName("GetNavigation")]
        public IHttpActionResult GetNavigation(int? roleId)
        {
            try
            {
                SqlParameter[] param = {
                                        new SqlParameter("@roleId",roleId == null?0:roleId), 
                                       };
                var data = _spService.ExcuteSpAnonmious("prc_getMenu", param, 1).Tables[0].AsEnumerable().GroupBy(g => g.Field<string>("Category")).Select(s => new
                {
                    Icon = s.First().Field<string>("Icon"),
                    Category = s.First().Field<string>("Category"),
                    Url = s.First().Field<string>("Url"),
                    Actions = s.Select(ss => new
                    {
                        Action = ss.Field<string>("Action"),
                        Url = ss.Field<string>("Url"),

                    }).ToList(),

                }).ToList();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }



   






        #endregion
    }
}
