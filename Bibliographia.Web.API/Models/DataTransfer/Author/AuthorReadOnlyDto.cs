using Bibliographia.Web.API.Models.DataTransfer;
using System.ComponentModel.DataAnnotations;

namespace Bibliographia.Web.API.Models.DataTransfer.Author
{
    public class AuthorReadOnlyDto : BaseDto
    {
        [Required, StringLength(50)]
        public string FirstName { get; set; }
        [Required, StringLength(50)]

        public string LastName { get; set; }
        [StringLength(500)]

        public string Bio { get; set; } = string.Empty;
    }
}
