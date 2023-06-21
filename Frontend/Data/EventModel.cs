namespace Frontend.Data;

public class EventModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime Date { get; set; }
    public string Location { get; set; } = null!;
    public string Description { get; set; } = null!;
    public virtual string Category { get; set; }
}