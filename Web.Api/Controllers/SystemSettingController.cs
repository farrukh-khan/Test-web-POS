using DataAccess.BLL;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Web.Api.Controllers
{
    public class SystemSettingController : ApiController
    {

        #region Fields
        private readonly ISystemSettingService _systemSettingervice;

        #endregion

        #region Ctor
        public SystemSettingController()
        {
            _systemSettingervice = StructureMap.ObjectFactory.GetInstance<ISystemSettingService>();
        }
        #endregion

        #region Api Controllers



        /// <summary>
        /// SystemSetting section start
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Authorize]
        [ActionName("GetSystemSettings")]

        public IHttpActionResult GetSystemSettings(int iCustomer)
        {
            try
            {
                //var systemSettings = _systemSettingervice.GetSystemSettings(x => x.ICustomer == iCustomer).FirstOrDefault();
                return Ok();
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpGet]
        [Authorize]
        [ActionName("GetSystemSettingById")]

        public IHttpActionResult GetSystemSettingById(int id)
        {
            try
            {
                var role = _systemSettingervice.GetSystemSettings(x => x.Id == id).FirstOrDefault();
                return Ok(role);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }


        [HttpPost]
        [Authorize]
        [ActionName("SystemSettingSubmit")]

        public IHttpActionResult SystemSettingSubmit(SystemSetting model)
        {
            try
            {
                //if (model != null)
                //{
                //    var exisData = _systemSettingervice.GetSystemSettings(x => x.ICustomer == model.ICustomer).FirstOrDefault();

                //    if (exisData != null)
                //    {
                //        exisData.LocalCode = model.LocalCode;
                //        exisData.AnswerTime = model.AnswerTime;
                //        exisData.MaxCallDuration = model.MaxCallDuration;
                //        _systemSettingervice.UpdateSystemSetting(exisData);
                //    }
                //    else
                //    {
                //        _systemSettingervice.InsertSystemSetting(model);
                //    }

                //}


                return Ok(true);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }





        #endregion
    }
}
