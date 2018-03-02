using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Api.Models.Permission
{
    public class PermissionModel
    {

        public long PermissionId { get; set; }
        public string Permission { get; set; }

        public bool IsAllowed { get; set; }

        public long RolePermissionId { get; set; }

        
        
    }
}