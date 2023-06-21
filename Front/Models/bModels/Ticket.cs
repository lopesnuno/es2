namespace Front.Models.bModels;

public class Ticket
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string QtyAvailable { get; set; } = null!;
    public Guid EventId { get; set; }
    public Event Event { get; set; }
