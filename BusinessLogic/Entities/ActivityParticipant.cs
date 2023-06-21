using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Entities;

public class ActivityParticipant
{
    [Key]
    public Guid ActivityId { get; set; }

    [Key]
    public Guid ParticipantId { get; set; }

    public Activity Activity { get; set; }

    public Participant Participant { get; set; }
}