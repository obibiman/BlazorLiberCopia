using System.ComponentModel.DataAnnotations;

namespace Bibliographia.Web.API.Models.DataTransfer.Publisher
{
    public class PublisherUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string? Name { get; set; }
        [StringLength(50)]
        public string? Location { get; set; }
        [StringLength(100)]
        public string? CompanyUrl { get; set; }
    }
}
