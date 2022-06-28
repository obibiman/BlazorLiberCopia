using System;
using System.Collections.Generic;

namespace Bibliographia.Web.API.Models.Domain
{
    public partial class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Year { get; set; }
        public string? Isbn { get; set; }
        public string? Genre { get; set; }
        public int? Pages { get; set; }
        public string? ImageUrl { get; set; }
        public decimal? Price { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public int? AuthorId { get; set; }

        public virtual Author? Author { get; set; }
    }
}
