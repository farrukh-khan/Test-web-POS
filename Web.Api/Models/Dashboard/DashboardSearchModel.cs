using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Api.Models.Dashboard
{
    public class DashboardSearchModel
    {


        public string FsYear { get; set; }
        public int GrantId { get; set; }
        public int Month { get; set; }

        public int Status { get; set; }


    }
}