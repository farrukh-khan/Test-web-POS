using FastReport;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;


namespace Web.Api.Common
{
    public class GenerateReport
    {
        
        public static string GetReport(string reportName,string title, List<ReportParameters> parameters, DataTable result)
        {
            try
            {
                Report report = new Report();
                string report_path = HttpContext.Current.Server.MapPath("~/ReportFormat/");
                report.Load(report_path + reportName+".frx");
                report.RegisterData(result, "Table");

                foreach (var param in parameters)
                {
                    report.SetParameterValue(param.Key, param.Value);
                }

                if (report.Prepare())
                {
                    FastReport.Utils.Config.WebMode = true;

                    // Set PDF export props
                    FastReport.Export.Pdf.PDFExport pdfExport = new FastReport.Export.Pdf.PDFExport();
                    pdfExport.ShowProgress = false;
                    pdfExport.Subject = title;
                    pdfExport.Title = title;
                    pdfExport.Compressed = true;
                    pdfExport.AllowPrint = true;
                    pdfExport.EmbeddingFonts = false;
                    pdfExport.Author = "Farrukh Khan";
                    pdfExport.Creator = "Fast Report";

                    var strm = new MemoryStream();
                    report.Export(pdfExport, strm);
                    report.Dispose();
                    pdfExport.Dispose();
                    strm.Position = 0;
                    
                    var fileStream = new FileStream(HttpContext.Current.Server.MapPath("~/Report/") + reportName + ".pdf", FileMode.Create, FileAccess.Write);
                    strm.CopyTo(fileStream);
                    fileStream.Dispose();

                }
                return reportName + ".pdf";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        public static string GetReportWithDs(string reportName,string title, List<ReportParameters> parameters, DataSet result)
        {
            try
            {
                Report report = new Report();
                string report_path = HttpContext.Current.Server.MapPath("~/ReportFormat/");
                report.Load(report_path + reportName+".frx");
                report.RegisterData(result.Tables[0], "Table");
               report.RegisterData(result.Tables[1], "Summary");
                
                foreach (var param in parameters)
                {
                    report.SetParameterValue(param.Key, param.Value);
                }

                if (report.Prepare())
                {
                    FastReport.Utils.Config.WebMode = true;

                    // Set PDF export props
                    FastReport.Export.Pdf.PDFExport pdfExport = new FastReport.Export.Pdf.PDFExport();
                    pdfExport.ShowProgress = false;
                    pdfExport.Subject = title;
                    pdfExport.Title = title;
                    pdfExport.Compressed = true;
                    pdfExport.AllowPrint = true;
                    pdfExport.EmbeddingFonts = false;
                    pdfExport.Author = "Farrukh Khan";
                    pdfExport.Creator = "Fast Report";

                    var strm = new MemoryStream();
                    report.Export(pdfExport, strm);
                    report.Dispose();
                    pdfExport.Dispose();
                    strm.Position = 0;
                    
                    var fileStream = new FileStream(HttpContext.Current.Server.MapPath("~/Report/") + reportName + ".pdf", FileMode.Create, FileAccess.Write);
                    strm.CopyTo(fileStream);
                    fileStream.Dispose();

                }
                return reportName + ".pdf";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetReport(string Reportname, string DataSetName, string Subject, string Title, List<ReportParameters> parameters, DataTable result)
        {
            try
            {


                Report report = new Report();
                string report_path = HttpContext.Current.Server.MapPath("~/ReportFormat/");
                string reportName = string.Empty;
                report.Load(report_path + Reportname+".frx");
                result.TableName = DataSetName;
                report.RegisterData(result, DataSetName);
              
                //foreach (var param in parameters)
                //{
                //    report.SetParameterValue(param.Key, param.Value);
                //}

                if (report.Prepare())
                {
                    FastReport.Utils.Config.WebMode = true;

                    // Set PDF export props
                    FastReport.Export.Pdf.PDFExport pdfExport = new FastReport.Export.Pdf.PDFExport();
                    pdfExport.ShowProgress = false;
                    pdfExport.Subject = Subject;
                    pdfExport.Title = Title;
                    pdfExport.Compressed = true;
                    pdfExport.AllowPrint = true;
                    pdfExport.EmbeddingFonts = false;
                    pdfExport.Author = "Farrukh Khan";
                    pdfExport.Creator = "Fast Report";

                    var strm = new MemoryStream();
                    report.Export(pdfExport, strm);
                    report.Dispose();
                    pdfExport.Dispose();
                    strm.Position = 0;
                    reportName = Reportname+DateTime.Now.ToString("dd_MM_yyyy_HH_mm");
                    if (File.Exists(HttpContext.Current.Server.MapPath("~/Report/") + reportName + ".pdf"))
                    {
                        File.Delete(HttpContext.Current.Server.MapPath("~/Report/") + reportName + ".pdf");
                    }
                    var fileStream = new FileStream(HttpContext.Current.Server.MapPath("~/Report/") + reportName + ".pdf", FileMode.Create, FileAccess.Write);
                    strm.CopyTo(fileStream);
                    fileStream.Dispose();

                }
                return reportName + ".pdf";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}