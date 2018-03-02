using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Api.Models.Patient
{
    public class PatientModel
    {
        public int Id { get; set; }
        public string Mr { get; set; }
        public string Prefix { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public Nullable<int> Age { get; set; }
        public string Sex { get; set; }
        public string Cnic { get; set; }
        public string Mobile { get; set; }
        public Nullable<int> RefLocationId { get; set; }
        public string RefName { get; set; }


    }
}