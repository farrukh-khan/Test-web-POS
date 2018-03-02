using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Api.Models.Permission
{
    public class PermissionAccess
    {

        public bool View { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }

        public bool IsAllowed { get; set; }

        public PermissionAccess()
        {
            View = false;
            Add = false;
            Edit = false;
            Delete = false;
            IsAllowed = false;
        }
    }
}