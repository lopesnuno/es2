using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Entities;

public class EventParticipant
{
    [Key]
    public Guid EventId { get; set; }

    [Key]
    public Guid ParticipantId { get; set; }

    public Event Event { get; set; }

    public Participant Participant { get; set; }
}