namespace GylleneDroppen.Admin.Blazor.Dtos.Event;

public class EventDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DateTime Date { get; init; }
    public TimeSpan StartTime { get; init; }
    public TimeSpan EndTime { get; init; }
    public string Location { get; init; } = string.Empty;
    public int MaxAttendees { get; init; }
    public int CurrentAttendees { get; init; }
    public decimal Price { get; init; }
    public bool IsPublic { get; init; }
    public string Status { get; init; } = string.Empty;
}