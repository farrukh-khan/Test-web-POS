using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Api.Models.Report
{
    public class ReportFilterModel
    {



        
        public string Code1 { get; set; }
        public string Code2 { get; set; }

        public string CustomerName { get; set; }

        public string BankName { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public int CallGapType { get; set; }

        public int AgeSlab { get; set; }
        public int MisType { get; set; }



        public int FeildFeedback { get; set; }

        public int OverallSummary { get; set; }
        
        
        public int ClientId { get; set; }

        public int ReportId { get; set; }


        public int Year { get; set; }

        public int Month { get; set; }

        public string Template { get; set; }



        public int PageSize { get; set; }

        public int Page { get; set; }



        public bool IsLoadPage { get; set; }


    }
}