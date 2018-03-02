using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Api.Models.Invoice
{
    public class ProductGroupModel
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public double Rate { get; set; }
        public int Qty { get; set; }
        public double Amount { get; set; }
        public string ReportDate { get; set; }
    }
}