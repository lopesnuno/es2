using System.Net;
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
        _httpClient.BaseAddress = new Uri("https://localhost:7199/");
    }

    public async Task GetEvents()
    {
        var result = await _httpClient.GetFromJsonAsync<List<Event>>("api/Event");

        if (result is not null)
            Events = result;
    }

    public async Task<Event?> GetEventById(string id)
    {
        var result = await _httpClient.GetAsync($"api/Event/{id}");

        if (result.StatusCode == HttpStatusCode.OK)
        {
            return await result.Content.ReadFromJsonAsync<Event>();
        }

        return null;
    }
}