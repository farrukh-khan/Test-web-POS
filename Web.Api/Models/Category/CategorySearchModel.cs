using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Api.Models.Shared;

namespace Web.Api.Models.Category
{
    public class CategorySearchModel
    {
        public long Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }

        public long CompanyId { get; set; }

        public int page { get; set; }
        public int pageSize { get; set; }

    }



}


