using GylleneDroppen.Admin.Blazor.Dtos.Admin;

namespace GylleneDroppen.Admin.Blazor.Services.Interfaces;

public interface IAdminService
{
    Task<List<AdminResponse>> GetAdminsAsync();
}