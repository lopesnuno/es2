using Front.Models.bModels;

namespace Front.Services;
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;

public class ActivityService : IActivityService
{
    private readonly HttpClient _httpClient;
    public List<Activity> Activities { get; set; } = new List<Activity>();
    private readonly NavigationManager _navigationManager;

    public ActivityService(HttpClient httpClient, NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        _httpClient = httpClient;
    }

    public async Task GetActivities()
    {
        var result = await _httpClient.GetFromJsonAsync<List<Activity>>("api/Activity");

        if (result is not null)
            Activities = result;
    }

    public async Task<Activity?> GetActivityById(string id)
    {
        var result = await _httpClient.GetAsync($"api/Activity/{id}");

        if (result.StatusCode == HttpStatusCode.OK)
        {
            return await result.Content.ReadFromJsonAsync<Activity>();
        }

        return null;
    }
    
    public async Task CreateActivity(Activity activity, string eventId)
    {
        activity.EventId = new Guid(eventId);
        var res = await _httpClient.PostAsJsonAsync("api/Activity", activity);

        if (res.StatusCode == HttpStatusCode.OK)
        {
            _navigationManager.NavigateTo($"/event/{eventId}");
        }
    }
    
    public async Task UpdateActivity(Activity activity)
    {
        await _httpClient.PutAsJsonAsync($"api/Activity/{activity.Id.ToString()}", activity);
    }
    
    public async Task DeleteActivity(string id)
    {
        await _httpClient.DeleteAsync($"api/Activity/{id}");
        _navigationManager.NavigateTo("/events");
    }

    public async Task BookParticipant(string activityId)
    {
        // TODO: Get user and add it
        var res = await _httpClient.GetAsync("api/User/f8551c2d-3172-4d56-af8e-e022d067e5e3");

        if (res.IsSuccessStatusCode)
        {
            await _httpClient.PostAsJsonAsync($"api/Activity/add-participant/{activityId}", res.Content.ReadFromJsonAsync<User>());
            _navigationManager.NavigateTo("/events");    
        }
    }
}