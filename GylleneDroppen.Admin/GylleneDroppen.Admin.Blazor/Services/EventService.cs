using System.Net.Http.Json;
using GylleneDroppen.Admin.Blazor.Dtos.Event;
using GylleneDroppen.Admin.Blazor.Services.Interfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace GylleneDroppen.Admin.Blazor.Services;

public class EventService(HttpClient httpClient) : IEventService
{
    public async Task<string?> CreateEvent(CreateEventRequest request)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, "api/events/create")
        {
            Content = JsonContent.Create(request)
        };

        httpRequest.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        var response = await httpClient.SendAsync(httpRequest);

        if (response.IsSuccessStatusCode) return null;
        
        var errorMessage = await response.Content.ReadAsStringAsync();
        return errorMessage;
    }
}