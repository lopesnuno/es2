namespace Front.Models.bModels;

public class Participant : User
{
    public ICollection<EventParticipant>? BookedEvents { get; set; }
}