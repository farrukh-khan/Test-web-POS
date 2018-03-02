using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Api.Models.Product
{
    public class ProductGroupSearchModel
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public string Search{ get; set; }

        public string Description { get; set; }
        public double SRate { get; set; }
        public int CompanyId { get; set; }
        
        public string CreatedBy { get; set; }
         
        public List<ProductSearchModel> Products { get; set; }

        public HttpPostedFileBase Img { get; set; }
    }

}