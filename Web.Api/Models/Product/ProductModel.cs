using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Api.Models.Shared;

namespace Web.Api.Models.Product
{
    public class ProductModel
    {
        public long Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double SaleRate { get; set; }
        public string Type { get; set; }
        
        public bool IsActive { get; set; }
        public bool IsContinue { get; set; }
        public int CompanyId { get; set; }
        public long CategoryId { get; set; }
        public bool IsAdd { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }

        public string CreatedBy { get; set; }



    }



}


