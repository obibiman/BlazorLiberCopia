using Bibliographia.BlazorUI.Server.ServiceClient.Base;

namespace Bibliographia.BlazorUI.Server.WebServicesProxy.Authentication
{
    public interface IAuthenticationService
    {
        public Task<bool> AuthenticateAsync(UserDto loginModel);
        public Task Logout();
    }
}
