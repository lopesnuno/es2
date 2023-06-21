using Front.Models.bModels;

namespace Front.Services;

public interface ITicketService
{
    List<EventTicket> Tickets { get; set; }
    Task GetTickets();
    Task<EventTicket?> GetTicketById(string id);
}

