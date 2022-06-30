using Microsoft.AspNetCore.Identity;

namespace Bibliographia.Web.API.Models.ApiGateway
{
    public class ApiUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
