using System.Net;
using System.Net.Http.Json;
using UserCrudApp.Models;
using Xunit;

namespace UserCrudApp.Tests;

public class UsersEndpointsTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public UsersEndpointsTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    private async Task<User> CreateUserAsync(string name = "Mario Rossi", string email = "mario.rossi@example.com")
    {
        var response = await _client.PostAsJsonAsync("/users", new CreateUserRequest(name, email));
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<User>())!;
    }

    [Fact]
    public async Task PostUsers_CreatesUser_ReturnsCreatedWithLocation()
    {
        var response = await _client.PostAsJsonAsync("/users", new CreateUserRequest("Anna Verdi", "anna.verdi@example.com"));

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var created = await response.Content.ReadFromJsonAsync<User>();
        Assert.NotNull(created);
        Assert.Equal("Anna Verdi", created!.Name);
        Assert.Equal($"/users/{created.Id}", response.Headers.Location?.OriginalString);
    }

    [Fact]
    public async Task GetUsers_ReturnsCreatedUser()
    {
        var user = await CreateUserAsync();

        var response = await _client.GetAsync("/users");

        response.EnsureSuccessStatusCode();
        var users = await response.Content.ReadFromJsonAsync<List<User>>();
        Assert.Contains(users!, u => u.Id == user.Id);
    }

    [Fact]
    public async Task GetUserById_Existing_ReturnsUser()
    {
        var user = await CreateUserAsync();

        var response = await _client.GetAsync($"/users/{user.Id}");

        response.EnsureSuccessStatusCode();
        var fetched = await response.Content.ReadFromJsonAsync<User>();
        Assert.Equal(user.Id, fetched!.Id);
    }

    [Fact]
    public async Task GetUserById_NotExisting_ReturnsNotFound()
    {
        var response = await _client.GetAsync("/users/999999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task PutUser_Existing_UpdatesUser()
    {
        var user = await CreateUserAsync();

        var response = await _client.PutAsJsonAsync($"/users/{user.Id}", new UpdateUserRequest("Mario Bianchi", "mario.bianchi@example.com"));

        response.EnsureSuccessStatusCode();
        var updated = await response.Content.ReadFromJsonAsync<User>();
        Assert.Equal("Mario Bianchi", updated!.Name);
        Assert.Equal("mario.bianchi@example.com", updated.Email);
    }

    [Fact]
    public async Task PutUser_NotExisting_ReturnsNotFound()
    {
        var response = await _client.PutAsJsonAsync("/users/999999", new UpdateUserRequest("X", "x@example.com"));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteUser_Existing_RemovesUser()
    {
        var user = await CreateUserAsync();

        var deleteResponse = await _client.DeleteAsync($"/users/{user.Id}");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        var getResponse = await _client.GetAsync($"/users/{user.Id}");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task DeleteUser_NotExisting_ReturnsNotFound()
    {
        var response = await _client.DeleteAsync("/users/999999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
