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
using System.Security.Principal;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Threading.Tasks;
using System.Data.OleDb;


namespace Web.Api.Controllers
{
    public class ImportController : ApiController
    {
       
    }
}
public class UploadDataModel
{
    public string testString1 { get; set; }
    public string testString2 { get; set; }
}