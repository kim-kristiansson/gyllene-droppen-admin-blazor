namespace GylleneDroppen.Admin.Blazor.Dtos.Event;

public class CreateEventRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int Capacity { get; set; }
    public int Price { get; set; }
    public DateTime Deadline { get; set; }
    public Guid OrganizerId { get; set; }
    public Guid CreatedById { get; set; }
}