namespace Front.Models.bModels;

public class Activity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime Date { get; set; }
    public string Description { get; set; } = null!;
    public Guid EventId { get; set; }
    public Event Event { get; set; }
    public ICollection<User>? Participants { get; set; } = new List<User>();
}