using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Api.Models.User
{
    public class UserModel
    {
        public long Id { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Nullable<long> RoleId { get; set; }
        public Nullable<int> ICustomer { get; set; }
        public byte UserType { get; set; }
        
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public bool IsLocked { get; set; }
        public bool IsSuspended { get; set; }
        public int InvalidPasswordAttempt { get; set; }
        public long CompanyId { get; set; }
        public int Code { get; set; }
        public bool IsCodeUsed { get; set; }

        public System.DateTime CodeExpiry { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    
    }
}