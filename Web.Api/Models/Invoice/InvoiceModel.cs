using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Api.Models.Invoice
{
    public class InvoiceModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Code { get; set; }
        public string Date { get; set; }
        public double SubTotal { get; set; }
        public double Discount { get; set; }
        public double OtherCharges { get; set; }
        public double Adjustment { get; set; }
        public double Total { get; set; }
    }
}