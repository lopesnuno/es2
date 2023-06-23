using System.Net;
using System.Net.Http.Json;
using Front.Models.bModels;
using Microsoft.AspNetCore.Components;

namespace Front.Services;

public class TicketService : ITicketService
{
    private readonly HttpClient _httpClient;
    public List<EventTicket> Tickets { get; set; } = new List<EventTicket>();
    private readonly NavigationManager _navigationManager;

    public TicketService(HttpClient httpClient, NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        _httpClient = httpClient;
    }

    public async Task GetTickets()
    {
        var result = await _httpClient.GetFromJsonAsync<List<EventTicket>>("api/EventTicker");

        if (result is not null)
            Tickets = result;
    }

    public async Task<EventTicket?> GetTicketById(string id)
    {
        var result = await _httpClient.GetAsync($"api/EventTicker/{id}");

        if (result.StatusCode == HttpStatusCode.OK)
        {
            return await result.Content.ReadFromJsonAsync<EventTicket>();
        }

        return null;
    }
    
    public async Task CreateTicket(EventTicket ticket, string eventId)
    {
        ticket.EventId = new Guid(eventId);
        var res = await _httpClient.PostAsJsonAsync("api/EventTicker", ticket);

        if (res.StatusCode == HttpStatusCode.OK)
        {
            _navigationManager.NavigateTo($"/event/{eventId}");
        }
    }
    
    public async Task UpdateTicket(EventTicket ticket)
    {
        await _httpClient.PutAsJsonAsync($"api/EventTicker/{ticket.Id.ToString()}", ticket);
    }
    
    public async Task DeleteTicket(string id)
    {
        await _httpClient.DeleteAsync($"api/EventTicker/{id}");
        _navigationManager.NavigateTo("/events");
    }
}