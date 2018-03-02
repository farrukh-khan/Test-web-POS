using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Api.Models.Permission;

namespace Web.Api.Models.Company
{
    public class RolePermissionModel
    {
        public int Id { get; set; }

        public long CompanyId { get; set; }
        public string RoleName { get; set; }

        public List<CategoryPermissionModel> PermissionList { get; set; }
    }
}