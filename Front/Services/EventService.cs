using System.Net;
using System.Net.Http.Json;
using Front.Models.bModels;
using Microsoft.AspNetCore.Components;

namespace Front.Services;

public class EventService : IEventService
{
    private readonly HttpClient _httpClient;
    private readonly NavigationManager _navigationManager;
    public List<Event> Events { get; set; } = new List<Event>();

    public EventService(HttpClient httpClient, NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        _httpClient = httpClient;
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

    public async Task UpdateEvent(Event newEventInfo)
    {
        await _httpClient.PutAsJsonAsync($"api/Event/{newEventInfo.Id.ToString()}", newEventInfo);
    }

    public async Task CreateEvent(Event newEvent)
    {
        var res = await _httpClient.PostAsJsonAsync("api/Event", newEvent);

        if (res.StatusCode == HttpStatusCode.OK)
        {
            _navigationManager.NavigateTo("/events");
        }
    }

    public async Task DeleteEvent(string id)
    {
        await _httpClient.DeleteAsync($"api/Event/{id}");
        _navigationManager.NavigateTo("/events");
    }
}