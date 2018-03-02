using DataAccess.BLL;
using Newtonsoft.Json;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using Web.Api.Common;
using Web.Api.Models.Log;
using System.Security.Principal;
using System.IO;

namespace Web.Api.Controllers
{
    public class LogController : ApiController
    {
        #region Fields
        
        private readonly ISpService _spService;
        private readonly ICompanyService _companyService;

        #endregion

        #region Ctor
        public LogController()
        {
         
            _spService = StructureMap.ObjectFactory.GetInstance<ISpService>();
            _companyService = StructureMap.ObjectFactory.GetInstance<ICompanyService>();

        }
        #endregion

        #region Api Controllers



        [HttpPost]
        [Authorize]
        [ActionName("FilterLog")]

        public IHttpActionResult FilterLog(LogModel model)
        {
            try
            {

                //DateTime startDate = DateTime.Parse(model.From,"",)

                SqlParameter[] param = {           
                                           new SqlParameter("@email",model.Email),                                                                                                  
                                           new SqlParameter("@clientId",model.ClientId),                                               
                                               new SqlParameter("@from",model.ClientId),                                               
                                               new SqlParameter("@to",model.Id),                                                                                              
                                       };
                var data = _spService.ExcuteSpAnonmious("prc_getLogs", param, 1);


                return Ok(data.Tables[0]);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }




        [HttpGet]
        [Authorize]
        [ActionName("GetLogById")]

        public IHttpActionResult GetLogById(int id, int? clientId)
        {
            try
            {
                //SqlParameter[] param = {           
                //                           new SqlParameter("@email",model.Email),                                                                                                  
                //                           new SqlParameter("@clientId",model.ClientId),                                               
                //                               new SqlParameter("@from",model.ClientId),                                               
                //                               new SqlParameter("@to",model.Id),                                                                                              
                //                       };
                //var data = _spService.ExcuteSpAnonmious("prc_getCases", param, 1);

                return Ok();
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }



        #endregion
    }
}
