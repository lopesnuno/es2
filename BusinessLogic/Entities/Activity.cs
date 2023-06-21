namespace BusinessLogic.Entities;

public class Activity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime Date { get; set; }
    public string Description { get; set; } = null!;
    public Guid EventId { get; set; }
    public Event Event { get; set; }
    public ICollection<ActivityParticipant>? Participants { get; set; } = new List<ActivityParticipant>();
}