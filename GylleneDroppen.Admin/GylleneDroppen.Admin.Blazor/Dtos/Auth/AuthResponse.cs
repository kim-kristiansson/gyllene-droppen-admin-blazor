namespace GylleneDroppen.Admin.Blazor.Dtos.Auth;

public class AuthResponse
{
    public Guid Id { get; init; } 
    public string Email { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string AccessToken { get; init; } = string.Empty;
    public string RefreshToken { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
}