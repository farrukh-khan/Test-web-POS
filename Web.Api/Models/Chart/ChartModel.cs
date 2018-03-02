using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Api.Models.Chart
{
    public class ChartModel
    {
        public string Label {get;set;}
        public string Color {get;set;}
        public List<ChartDetailModel> Data { get; set; }

        public ChartModel()
        {
            Label = "";
            Color = "";
            Data = new List<ChartDetailModel>();
        }
    }

   


}
