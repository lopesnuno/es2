namespace BusinessLogic.Entities;

public class EventParticipant
{
    public Guid EventId { get; set; }
    public Event Event { get; set; }
    public Participant Participant { get; set; }
    public Guid ParticipantId { get; set; }
}