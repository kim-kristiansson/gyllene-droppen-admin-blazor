using System.Net.Http.Json;
using Blazored.LocalStorage;
using GylleneDroppen.Admin.Blazor.Dtos.Auth;
using GylleneDroppen.Admin.Blazor.Providers;
using GylleneDroppen.Admin.Blazor.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace GylleneDroppen.Admin.Blazor.Services;

public class AuthService(
    HttpClient httpClient,
    ILocalStorageService localStorageService,
    CustomAuthenticationStateProvider authStateProvider,
    NavigationManager navigationManager) : IAuthService
{
    public async Task<bool> Login(LoginRequest loginRequest)
    {
        var response = await httpClient.PostAsJsonAsync("api/auth/login", loginRequest);

        if (!response.IsSuccessStatusCode)
            return false;

        var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();

        if (authResponse?.Role is not "Admin")
            return false;
        

        await authStateProvider.MarkUserAsAuthenticated(authResponse.AccessToken);

        navigationManager.NavigateTo("/", forceLoad: true);
        return true;
    }

    public async Task Logout()
    {
        await authStateProvider.MarkUserAsLoggedOut();
        navigationManager.NavigateTo("/login", forceLoad: true);
    }

    public async Task<string?> GetToken()
    {
        return await localStorageService.GetItemAsync<string>("accessToken");
    }
}
