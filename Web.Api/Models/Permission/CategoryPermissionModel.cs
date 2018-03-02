using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Api.Models.Permission
{
    public class CategoryPermissionModel
    {
        public string Category { get; set; }
        public string Role { get; set; }
        public long RoleId { get; set; }

        public List<ActionPermissionModel> Actions { get; set; }

    }
}