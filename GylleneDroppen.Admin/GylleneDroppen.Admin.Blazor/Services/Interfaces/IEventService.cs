using GylleneDroppen.Admin.Blazor.Dtos.Event;

namespace GylleneDroppen.Admin.Blazor.Services.Interfaces;

public interface IEventService
{
    Task<string?> CreateEvent(CreateEventRequest request);
}