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
    
    public partial class Company
    {
        public Company()
        {
            this.Invoice = new HashSet<Invoice>();
            this.Lab = new HashSet<Lab>();
            this.Log = new HashSet<Log>();
            this.Role = new HashSet<Role>();
            this.Voucher = new HashSet<Voucher>();
        }
    
        public long Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public Nullable<long> CountryId { get; set; }
        public bool IsActive { get; set; }
    
        public virtual Company Company1 { get; set; }
        public virtual Company Company2 { get; set; }
        public virtual ICollection<Invoice> Invoice { get; set; }
        public virtual ICollection<Lab> Lab { get; set; }
        public virtual ICollection<Log> Log { get; set; }
        public virtual ICollection<Role> Role { get; set; }
        public virtual ICollection<Voucher> Voucher { get; set; }
    }
}
