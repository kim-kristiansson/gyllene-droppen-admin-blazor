using System.Net.Http.Json;
using GylleneDroppen.Admin.Blazor.Dtos.Admin;
using GylleneDroppen.Admin.Blazor.Services.Interfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace GylleneDroppen.Admin.Blazor.Services;

public class AdminService(HttpClient httpClient) : IAdminService
{
    public async Task<List<AdminResponse>> GetAdminsAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/admins");
        request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        var response = await httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            return [];
        }

        return await response.Content.ReadFromJsonAsync<List<AdminResponse>>() ?? new List<AdminResponse>();
    }
}