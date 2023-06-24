using System.Net;
using System.Net.Http.Json;
using Front.Models.bModels;
using Microsoft.AspNetCore.Components;

namespace Front.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    private readonly NavigationManager _navigationManager;
    public List<User> Users { get; set; } = new List<User>();

    public UserService(HttpClient httpClient, NavigationManager navigationManager)
    {
        _httpClient = httpClient;
        _navigationManager = navigationManager;
    }

    public async Task GetUsers()
    {
        var result = await _httpClient.GetFromJsonAsync<List<User>>("api/User");

        if (result is not null)
            Users = result;
    }

    public async Task<User?> GetUserById(string id)
    {
        var result = await _httpClient.GetAsync($"api/User/{id}");

        if (result.StatusCode == HttpStatusCode.OK)
        {
            return await result.Content.ReadFromJsonAsync<User>();
        }

        return null;
    }
    
    public async Task CreateUser(User user)
    {
        var res = await _httpClient.PostAsJsonAsync("api/User", user);

        if (res.StatusCode == HttpStatusCode.OK)
        {
            _navigationManager.NavigateTo("/events");
        }
    }
    
    public async Task UpdateUser(User user)
    {
        var res = await _httpClient.PutAsJsonAsync($"api/User/{user.Id.ToString()}", user);
        
        if (res.StatusCode == HttpStatusCode.OK)
        {
            _navigationManager.NavigateTo("/users");
        }
    }
    
    public async Task DeleteUser(string id)
    {
        await _httpClient.DeleteAsync($"api/User/{id}");
        _navigationManager.NavigateTo("/users");
    }
}