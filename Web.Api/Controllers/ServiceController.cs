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
using Web.Api.Models.Permission;
using Web.Api.Models.User;

namespace Web.Api.Controllers
{
    public class ServiceController : ApiController
    {

        #region Fields
        
        private readonly ISpService _spService;
        
        #endregion

        #region Ctor
        public ServiceController()
        {            
            _spService = StructureMap.ObjectFactory.GetInstance<ISpService>();            
        }
        #endregion

        #region Api Controllers



        /// <summary>
        /// Service section start
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [ActionName("SpCall")]

        public IHttpActionResult SpCall()
        {
            try
            {
                SqlParameter[] param = {                                        
                                       };
                _spService.ExcuteSpAnonmious("spCalls", param, 1);

                return Ok("SpCalls called Successfully.");
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }



        #endregion
    }
}
