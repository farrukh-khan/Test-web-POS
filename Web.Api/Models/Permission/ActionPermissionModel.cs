using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Api.Models.Permission
{
    public class ActionPermissionModel
    {
        public long ActionId { get; set; }
        public string Action { get; set; }

        public List<PermissionModel> PermissionModel { get; set; }
    }
}