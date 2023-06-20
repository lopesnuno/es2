using System.Net.Http.Json;
using Front.Models.bModels;

namespace Front.Services;

public class EventService : IEventService
{
    private readonly HttpClient _httpClient;
    public List<Event> Events { get; set; } = new List<Event>();

    public EventService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task GetEvents()
    {
        _httpClient.BaseAddress = new Uri("https://localhost:7199/");
        
        var result = await _httpClient.GetFromJsonAsync<List<Event>>("api/Event");

        if (result is not null)
            Events = result;
    }

    public Task<Event?> GetEventById(string id)
    {
        throw new NotImplementedException();
    }
}