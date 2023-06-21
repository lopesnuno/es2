using Front.Models.bModels;

namespace Front.Services;

public interface IEventService
{
    List<Event> Events { get; set; }
    Task GetEvents();
    Task<Event?> GetEventById(string id);
    Task UpdateEvent(Event newEventInfo);
}