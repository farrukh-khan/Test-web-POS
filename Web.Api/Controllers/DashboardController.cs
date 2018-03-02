using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;
using Web.Api.Common;
using Web.Api.Models.Chart;

using Web.Api.Models.Dashboard;


namespace Web.Api.Controllers
{
    public class DashboardController : ApiController
    {

        #region Fields
        private readonly ISpService _spService;

        #endregion

        #region Ctor
        public DashboardController()
        {
            _spService = StructureMap.ObjectFactory.GetInstance<ISpService>();

        }
        #endregion

        #region Api Controllers

        [HttpPost]
        [Authorize]
        [ActionName("GetDashboard")]

        public IHttpActionResult GetDashboard(DashboardSearchModel model)
        {
            try
            { 

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


