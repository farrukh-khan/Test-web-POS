using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Api.Models.Log
{
    public class LogModel
    {
        public int Id { get; set; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public string Email { get; set; }
        public int ClientId { get; set; }
    }
}