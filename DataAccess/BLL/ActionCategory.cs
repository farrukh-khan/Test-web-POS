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
    
    public partial class ActionCategory
    {
        public ActionCategory()
        {
            this.Action = new HashSet<Action>();
        }
    
        public long Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public string Icon { get; set; }
    
        public virtual ICollection<Action> Action { get; set; }
    }
}