using System.Net.Http.Json;
using System.Security.Claims;
using Blazored.LocalStorage;
using GylleneDroppen.Admin.Blazor.Dtos.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace GylleneDroppen.Admin.Blazor.Providers;

public class CustomAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
    : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = await GetCurrentUserAsync();
        return new AuthenticationState(user);
    }

    public async Task MarkUserAsAuthenticated()
    {
        var user = await GetCurrentUserAsync();
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public void MarkUserAsLoggedOut()
    {
        var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));
    }

    private async Task<ClaimsPrincipal> GetCurrentUserAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/auth/me");
        request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        var response = await httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            return new ClaimsPrincipal(new ClaimsIdentity());

        var claims = await response.Content.ReadFromJsonAsync<List<ClaimResponse>>();

        if (claims is null || claims.Count == 0)
            return new ClaimsPrincipal(new ClaimsIdentity());

        var identity = new ClaimsIdentity(claims.Select(c => new Claim(c.Type, c.Value)), "cookie");
        return new ClaimsPrincipal(identity);
    }

    public void NotifyAuthStateChanged()
    {
        var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));
    }
}
