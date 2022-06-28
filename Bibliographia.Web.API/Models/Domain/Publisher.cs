using System;
using System.Collections.Generic;

namespace Bibliographia.Web.API.Models.Domain
{
    public partial class Publisher
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public string? CompanyUrl { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
