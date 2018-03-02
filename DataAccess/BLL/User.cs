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
    
    public partial class User
    {
        public User()
        {
            this.UserLogin = new HashSet<UserLogin>();
        }
    
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Nullable<long> RoleId { get; set; }
        public bool IsActive { get; set; }
        public bool IsLocked { get; set; }
        public bool IsSuspended { get; set; }
        public int InvalidPasswordAttempt { get; set; }
        public int Code { get; set; }
        public bool IsCodeUsed { get; set; }
        public System.DateTime CodeExpiry { get; set; }
        public bool IsContinue { get; set; }
        public long CompanyId { get; set; }
        public int LabId { get; set; }
        public bool IsAdd { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
    
        public virtual ICollection<UserLogin> UserLogin { get; set; }
    }
}
