using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Bibliographia.Web.API.Models.DataTransfer.Book
{
    public class BookUpdateDto : BaseDto
    {
        [Required, StringLength(100)]
        public string? Title { get; set; }
        [StringLength(4)]
        public string? Year { get; set; }
        [StringLength(15)]
        public string? Isbn { get; set; }
        [Required, StringLength(250, MinimumLength = 10)]
        public string? ImageUrl { get; set; }
        [Required, Range(0, int.MaxValue)]
        public decimal? Price { get; set; }
        public DateTime? Updated { get; set; }
    }
}

