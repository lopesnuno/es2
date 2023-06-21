using System.Net;
using System.Net.Http.Json;
using Front.Models.bModels;

namespace Front.Services;

public class TicketService : ITicketService
{
    private readonly HttpClient _httpClient;
    public List<EventTicket> Tickets { get; set; } = new List<EventTicket>();

    public TicketService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:7199/");
    }

    public async Task GetTickets()
    {
        var result = await _httpClient.GetFromJsonAsync<List<EventTicket>>("api/EventTicket");

        if (result is not null)
            Tickets = result;
    }

    public async Task<EventTicket?> GetTicketById(string id)
    {
        var result = await _httpClient.GetAsync($"api/EventTicket/{id}");

        if (result.StatusCode == HttpStatusCode.OK)
        {
            return await result.Content.ReadFromJsonAsync<EventTicket>();
        }

        return null;
    }
}