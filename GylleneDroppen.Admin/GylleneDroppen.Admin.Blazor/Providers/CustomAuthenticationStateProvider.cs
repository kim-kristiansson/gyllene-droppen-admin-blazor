using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace GylleneDroppen.Admin.Blazor.Providers;

public class CustomAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
    : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await localStorage.GetItemAsync<string>("accessToken");

        if (string.IsNullOrEmpty(token))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var claims = ParseClaimsFromJwt(token);
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

        return new AuthenticationState(user);
    }

    public async Task MarkUserAsAuthenticated(string token)
    {
        await localStorage.SetItemAsync("accessToken", token);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var claims = ParseClaimsFromJwt(token);
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user))); // ✅ Fix
    }

    public async Task MarkUserAsLoggedOut()
    {
        await localStorage.RemoveItemAsync("accessToken");
        httpClient.DefaultRequestHeaders.Authorization = null;

        var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser))); // ✅ Fix
    }

    private static List<Claim> ParseClaimsFromJwt(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);

        if (jwt.Claims == null || !jwt.Claims.Any())
        {
            return [];
        }

        var claims = new List<Claim>(jwt.Claims);

        var roleClaims = jwt.Claims.Where(c => c.Type is ClaimTypes.Role or "role")
            .Select(c => new Claim(ClaimTypes.Role, c.Value));
        claims.AddRange(roleClaims);

        return claims;
    }
}
