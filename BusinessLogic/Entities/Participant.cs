namespace BusinessLogic.Entities;

public class Participant : User
{
    public ICollection<ActivityParticipant>? BookedActivities { get; set; }
}