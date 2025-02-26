namespace GylleneDroppen.Admin.Blazor.Dtos.Auth;

public class ClaimResponse
{
    public required string Type { get; init; }
    public required string Value { get; init; }
}