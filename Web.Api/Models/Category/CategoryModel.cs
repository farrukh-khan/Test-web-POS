using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Api.Models.Shared;

namespace Web.Api.Models.Category
{
    public class CategoryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsContinue { get; set; }
        public int CompanyId { get; set; }
        public bool IsAdd { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }


    }



}


