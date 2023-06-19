namespace BusinessLogic.Entities;

public class Participant : User
{
    public ICollection<EventParticipant> BookedEvent { get; set; }
}