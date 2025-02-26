using System.Net.Http.Json;
using Blazored.LocalStorage;
using GylleneDroppen.Admin.Blazor.Dtos.Auth;
using GylleneDroppen.Admin.Blazor.Providers;
using GylleneDroppen.Admin.Blazor.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace GylleneDroppen.Admin.Blazor.Services;

public class AuthService(
    HttpClient httpClient,
    ILocalStorageService localStorageService,
    CustomAuthenticationStateProvider authStateProvider,
    NavigationManager navigationManager) : IAuthService
{
    public async Task<string?> Login(LoginRequest loginRequest)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "api/auth/login")
        {
            Content = JsonContent.Create(loginRequest)
        };

        request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        var response = await httpClient.SendAsync(request);
    
        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            return errorMessage;
        }

        var authState = await authStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        
        if(user.Identity is not {IsAuthenticated: true})
            return "Login failed. Please try again.";
        
        if(!user.IsInRole("Admin"))
            return "You are not authorized to access the admin panel.";
        
        navigationManager.NavigateTo("/", forceLoad: true);
        return null;
    }


    public async Task Logout()
    {
        await httpClient.PostAsync("api/auth/logout", null);
        authStateProvider.MarkUserAsLoggedOut();
    
        await authStateProvider.GetAuthenticationStateAsync();

        navigationManager.NavigateTo("/login", forceLoad: true);
    }


    public async Task<string?> GetToken()
    {
        return await localStorageService.GetItemAsync<string>("accessToken");
    }
}
