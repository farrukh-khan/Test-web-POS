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
    
    public partial class LabMap
    {
        public long Id { get; set; }
        public long LabId { get; set; }
        public long LabCategoryId { get; set; }
    
        public virtual Lab Lab { get; set; }
        public virtual LabCategory LabCategory { get; set; }
    }
}