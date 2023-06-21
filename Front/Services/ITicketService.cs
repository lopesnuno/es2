using Front.Models.bModels;

namespace Front.Services;

public interface ITicketService
{
    List<Ticket> Ticket { get; set; }
    Task GetTickets();
    Task<Ticket?> GetTicketById(string id);
}

