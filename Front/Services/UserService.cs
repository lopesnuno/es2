using System.Net;
using System.Net.Http.Json;
using Front.Models.bModels;

namespace Front.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    public List<User> Users { get; set; } = new List<User>();

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:7199/");
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
}