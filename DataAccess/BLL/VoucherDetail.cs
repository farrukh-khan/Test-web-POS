//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.BLL
{
    using System;
    using System.Collections.Generic;
    
    public partial class VoucherDetail
    {
        public long Id { get; set; }
        public long VoucherId { get; set; }
        public string AccountId { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public bool IsActive { get; set; }
        public string Type { get; set; }
        public string BillNo { get; set; }
        public string Particular { get; set; }
    
        public virtual ChartofAccount ChartofAccount { get; set; }
        public virtual Voucher Voucher { get; set; }
    }
}