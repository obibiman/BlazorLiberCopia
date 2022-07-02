using System.ComponentModel.DataAnnotations;

namespace Bibliographia.Web.API.Models.DataTransfer.User
{
    public class LoginUserDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
