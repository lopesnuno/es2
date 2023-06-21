namespace Front.Models.bModels;

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
    public virtual ICollection<EventTicket>? Tickets { get; set; } = new List<EventTicket>();
    public virtual ICollection<Activity>? Activities { get; set; } = new List<Activity>();
    public virtual ICollection<EventParticipant>? Participants { get; set; } = new List<EventParticipant>();
}