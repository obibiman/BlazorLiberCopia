using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Bibliographia.BlazorUI.Server.Providers
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private readonly ILogger<ApiAuthenticationStateProvider> _logger;
        public ApiAuthenticationStateProvider(ILocalStorageService localStorageService, ILogger<ApiAuthenticationStateProvider> logger)
        {
            _localStorageService = localStorageService;
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            _logger = logger;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            _logger.LogInformation($"create instance of ClaimsPrincipal in {nameof(GetAuthenticationStateAsync)}");
            var user = new ClaimsPrincipal(new ClaimsIdentity());

            _logger.LogInformation($"retrieving taken from local storage in {nameof(GetAuthenticationStateAsync)}");
            //retrieve token from local storage
            var savedToken = await _localStorageService.GetItemAsync<string>("accessToken");
            if (savedToken == null)
            {
                _logger.LogWarning($"there was no token to be retrieved in {nameof(GetAuthenticationStateAsync)}");
                return new AuthenticationState(user);
            }

            _logger.LogInformation($"token was retrieved in {nameof(GetAuthenticationStateAsync)} in and will be validated.");
            //read token contents
            var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(savedToken);
            if (tokenContent.ValidTo < DateTime.Now)
            {
                _logger.LogWarning($"token was retrieved in {nameof(GetAuthenticationStateAsync)} but had already expired.");
                return new AuthenticationState(user);
                //await _localStorageService.RemoveItemAsync("accessToken");
            }
            _logger.LogInformation($"retrieving claims in {nameof(GetAuthenticationStateAsync)}.");
            //get claims
            var claims = await GetClaims();

            _logger.LogInformation($"claims {claims} have been retrieved.");
            //get user
            user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            _logger.LogInformation($"user details {user} have been retrieved.");
            return new AuthenticationState(new ClaimsPrincipal(user));
        }

        public async Task LoggedIn()
        {
            var claims = await GetClaims();
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            var authState = Task.FromResult(new AuthenticationState(user));
            NotifyAuthenticationStateChanged(authState);
        }

        public async Task LoggedOut()
        {
            await _localStorageService.RemoveItemAsync("accessToken");
            var nobody = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(nobody));
            NotifyAuthenticationStateChanged(authState);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var savedToken = await _localStorageService.GetItemAsync<string>("accessToken");
            var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(savedToken);
            var claims = tokenContent.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
            return claims;
        }
    }
}
