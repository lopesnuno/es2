using Front.Models.bModels;
using Front.Pages;

namespace Front.Services;

public interface ITicketService
{
    List<EventTicket> Tickets { get; set; }
    Task GetTickets();
    Task<EventTicket?> GetTicketById(string id);
    Task CreateTicket(EventTicket ticket, string eventId);
    Task UpdateTicket(EventTicket ticket);
}

