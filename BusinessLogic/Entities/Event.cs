namespace BusinessLogic.Entities;

public class Event
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateOnly Date { get; set; }
    public string Location { get; set; } = null!;
    public string Description { get; set; } = null!;
    public virtual EventCategory Category { get; set; }
    public Guid OrganizerId { get; set; }
    public Organizer Organizer { get; set; }
    public ICollection<TicketType> TicketTypes { get; set; } = new List<TicketType>();
    public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    public ICollection<EventParticipant> BookedEvent { get; set; }
}