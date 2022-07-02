using Bibliographia.BlazorUI.Server.Providers;
using Bibliographia.BlazorUI.Server.WebServicesProxy.Base;
using Blazored.LocalStorage;

namespace Bibliographia.BlazorUI.Server.WebServicesProxy.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IBibliographiaClient _bibliographicClient;
        private readonly ILocalStorageService _localStorageService;
        private readonly ApiAuthenticationStateProvider _stateProvider;
        public AuthenticationService(IBibliographiaClient bibliographicClient, ILocalStorageService localStorage, ApiAuthenticationStateProvider stateProvider)
        {
            _bibliographicClient = bibliographicClient;
            _localStorageService = localStorage;
            _stateProvider = stateProvider;
        }
        public async  Task<bool> AuthenticateAsync(UserDto loginModel)
        {
            var response = await _bibliographicClient.LoginAsync(loginModel);
            await _localStorageService.SetItemAsync("accessToken", response.Token);
            //store token

            //change auth state of app
          await _stateProvider.LoggedIn();
            return true;
        }

        public async Task Logout()
        {
            await _stateProvider.LoggedOut();
        }
    }
}
