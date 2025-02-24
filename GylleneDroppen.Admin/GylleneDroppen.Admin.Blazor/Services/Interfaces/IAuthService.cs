using GylleneDroppen.Admin.Blazor.Dtos.Auth;

namespace GylleneDroppen.Admin.Blazor.Services.Interfaces;

public interface IAuthService
{
    Task<bool> Login(LoginRequest loginRequest);
    Task Logout();
    Task<string?> GetToken();
}