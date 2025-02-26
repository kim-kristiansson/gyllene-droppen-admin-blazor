using GylleneDroppen.Admin.Blazor.Dtos.Auth;

namespace GylleneDroppen.Admin.Blazor.Services.Interfaces;

public interface IAuthService
{
    Task<string?> Login(LoginRequest loginRequest);
    Task Logout();
    Task<string?> GetToken();
}