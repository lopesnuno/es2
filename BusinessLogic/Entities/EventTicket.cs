namespace BusinessLogic.Entities;

public class EventTicket
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public int QtyAvailable { get; set; }
    public decimal Price { get; set; }
    public Guid EventId { get; set; }
    public Event Event { get; set; }
}