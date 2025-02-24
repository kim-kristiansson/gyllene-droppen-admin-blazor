using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GylleneDroppen.Admin.Blazor;
using GylleneDroppen.Admin.Blazor.Providers;
using GylleneDroppen.Admin.Blazor.Services;
using GylleneDroppen.Admin.Blazor.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register services
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5027/") // Change this to your actual API URL
});

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<CustomAuthenticationStateProvider>(); // Ensure DI works
builder.Services.AddScoped<IAuthService, AuthService>();

await builder.Build().RunAsync();