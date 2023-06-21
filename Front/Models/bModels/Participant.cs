using BusinessLogic.Entities;

namespace Front.Models.bModels;

public class Participant : User
{
    public ICollection<ActivityParticipant>? BookedActivities { get; set; }
}