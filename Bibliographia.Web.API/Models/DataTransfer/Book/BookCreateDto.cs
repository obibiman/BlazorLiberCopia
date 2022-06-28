using System.ComponentModel.DataAnnotations;

namespace Bibliographia.Web.API.Models.DataTransfer.Book
{
    public class BookCreateDto
    {
        [Required, StringLength(100)]
        public string? Title { get; set; }
        [StringLength(4)]
        public string? Year { get; set; }
        [StringLength(15)]
        public string? Isbn { get; set; }
        [StringLength(25)]
        public string? Genre { get; set; }
        public int? Pages { get; set; }
        [StringLength(100)]
        public string? ImageUrl { get; set; }
        [Required, Range(0, int.MaxValue)]
        public decimal? Price { get; set; }
    }
}