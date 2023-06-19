namespace BusinessLogic.Entities;

public class Participant : User
{
    public ICollection<EventParticipant>? BookedEvents { get; set; }
}