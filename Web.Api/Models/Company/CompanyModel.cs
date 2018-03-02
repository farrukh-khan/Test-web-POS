using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Api.Models.Permission;
using Web.Api.Models.User;

namespace Web.Api.Models.Company
{
    public class CompanyModel
    {
        public bool IsActive { get; set; }
        public int Id { get; set; }
        public int ICustomer { get; set; }
        public string Name { get; set; }

        public string Rolename { get; set; }
        public string Description { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public Nullable<int> CountryId { get; set; }
        public UserModel User { get; set; }

        public List<CategoryPermissionModel> Permission { get; set; }
        
        
        
    }
}