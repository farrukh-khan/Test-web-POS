using DataAccess.BLL;
using Microsoft.Office.Interop.Excel;
using Service.Contracts;
using SmartXLS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using Web.Api.Common;
using Web.Api.Models.Report;

namespace Web.Api.Controllers
{
    public class ReportController : ApiController
    {
        #region Fields
        private readonly IReportCatalogueService _reportCatalogueService;
        private readonly IReportService _reportService;

        private readonly IInvoiceDetailService _caseTransactionService;


        private readonly ISpService _spService;



        #endregion

        #region Ctor
        public ReportController()
        {
            _reportCatalogueService = StructureMap.ObjectFactory.GetInstance<IReportCatalogueService>();
            _reportService = StructureMap.ObjectFactory.GetInstance<IReportService>();
            _spService = StructureMap.ObjectFactory.GetInstance<ISpService>();
            _caseTransactionService = StructureMap.ObjectFactory.GetInstance<IInvoiceDetailService>();


        }
        #endregion

        #region Api Controllers

        [HttpGet]
        [Authorize]
        [ActionName("GetReportCatalogues")]

        public IHttpActionResult GetReportCatalogues()
        {
            try
            {



                var reportCatalogue = _reportCatalogueService.GetReportCatalogues().Select(s => new ReportCatalogue
                    {
                        Id = s.Id,
                        Title = s.Title,
                        Report = _reportService.GetReports(x => x.ReportCatalogueId == s.Id).OrderBy(o => o.OrderNumber).ToList()
                    }).OrderBy(o => o.OrderNumber).ToList();
                return Ok(reportCatalogue);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }


        [HttpGet]
        [Authorize]
        [ActionName("GetReportInfo")]
        public IHttpActionResult GetReportInfo(int id, int? clientId)
        {
            try
            {


                SqlParameter[] param = {                                                          
                                             new SqlParameter("@reportId",id),                                                                                              
                                               new SqlParameter("@clientId",clientId)
                                               
                                       };

                var data = _spService.ExcuteSpAnonmious("prc_getReportInfo", param, 3);

                return Ok(data);

            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));

            }
        }




        [HttpPost]
        [Authorize]
        [ActionName("GetReport")]
        public IHttpActionResult GetReport(ReportFilterModel model)
        {
            try
            {
                var report = _reportService.GetReportById(model.ReportId);
                var data = GetReportData(model);
                return Ok(new ArrayList { data, report.Title });


            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));

            }
        }

        private DataSet GetReportData(ReportFilterModel model)
        {
            string proc = "prc_ReportExecute";
            string query = string.Empty;



            string prcName = "prc_report";

            string startDate = "";
            string endDate = "";
            if (model.Template.ToUpper() == "ISYEARMONTH")
            {
                startDate = new DateTime(model.Year, model.Month, 1).ToString();
                endDate = new DateTime(model.Year, model.Month, DateTime.DaysInMonth(model.Year, model.Month)).ToString();
            }
            if (model.Template.ToUpper() == "ISDATE")
            {
                if (!string.IsNullOrEmpty(model.From) && !string.IsNullOrEmpty(model.To))
                {
                    startDate = Convert.ToDateTime(model.From).ToString();
                    endDate = Convert.ToDateTime(model.To).ToString();
                }

            }


            query = string.Format("{0} '{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}'",
                prcName,
                model.ClientId,
                startDate,
                endDate,
                model.CallGapType,
                model.AgeSlab,
                model.FeildFeedback,
                model.OverallSummary,
                model.MisType,
                    model.Page == 1 ? 0 : (model.Page - 1) * model.PageSize,
                    model.PageSize,
                    "ASC",
                   model.ReportId
                 );


            SqlParameter[] param = {
                                            new SqlParameter("@CurrentPage","1"), 
                                            new SqlParameter("@PageSize","-1"),
                                            new SqlParameter("@Query",query),                                        
                                           };
            DataSet data = _spService.ExcuteSpAnonmious(proc, param, 3);

            return data;
        }



        [HttpPost]
        [Authorize]
        [ActionName("GetReportExcel")]
        public HttpResponseMessage GetReportExcel(ReportFilterModel model)
        {
            try
            {

                var report = _reportService.GetReportById(model.ReportId);
                var data = GetReportData(model);

                string filePath = ExcelUtility.createXSL(data, report.Title);

                byte[] file = System.IO.File.ReadAllBytes(filePath);
                var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new ByteArrayContent(file) };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = filePath
                };

                return response;

            }
            catch (Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;

            }
        }






        [HttpPost]
        [Authorize]
        [ActionName("GetReportPdf")]
        public HttpResponseMessage GetReportPdf(ReportFilterModel model)
        {
            try
            {
                var report = _reportService.GetReportById(model.ReportId);
                var data = GetReportData(model);

                // report start


                var parameters = new List<ReportParameters>();


                parameters.Add(new ReportParameters()
                {
                    Key = "CompanyName",
                    Value = ""
                });
                parameters.Add(new ReportParameters()
                {
                    Key = "Title",
                    Value = report.Title
                });




                string fileName = GenerateReport.GetReportWithDs(report.PdfReportName, "Report", parameters, data);
                byte[] file = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/Report/") + fileName);
                var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new ByteArrayContent(file) };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };

                return response;

            }
            catch (Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;

            }
        }





        #endregion


    }
}

