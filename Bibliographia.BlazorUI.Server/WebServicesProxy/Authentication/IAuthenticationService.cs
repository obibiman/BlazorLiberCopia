using Bibliographia.BlazorUI.Server.WebServicesProxy.Base;

namespace Bibliographia.BlazorUI.Server.WebServicesProxy.Authentication
{
    public interface IAuthenticationService
    {
        public Task<bool> AuthenticateAsync(UserDto loginModel);
        public Task Logout();
    }
}
