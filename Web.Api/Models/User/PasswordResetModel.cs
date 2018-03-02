using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Api.Models.User
{
    public class PasswordResetModel
    {
        public string Email { get; set; }
        public int Id { get; set; }
        public int Code { get; set; }
        
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        
    }
}