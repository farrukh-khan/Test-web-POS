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
    
    public partial class City
    {
        public City()
        {
            this.Area = new HashSet<Area>();
        }
    
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Nullable<int> CountryId { get; set; }
        public bool IsActive { get; set; }
        public bool IsContinue { get; set; }
        public int CompanyId { get; set; }
        public bool IsAdd { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual ICollection<Area> Area { get; set; }
    }
}
