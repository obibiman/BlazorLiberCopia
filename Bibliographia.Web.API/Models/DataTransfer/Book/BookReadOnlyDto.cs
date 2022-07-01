using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Bibliographia.Web.API.Models.DataTransfer.Book
{
    public class BookReadOnlyDto : BaseDto
    {
        public string? Title { get; set; }
        public string? Year { get; set; }
        public string? Genre { get; set; }
        public string? ImageUrl { get; set; }
        public decimal? Price { get; set; }
        public int AuthorId { get; set; }
        public string? AuthorName { get; set; }
    }
}


