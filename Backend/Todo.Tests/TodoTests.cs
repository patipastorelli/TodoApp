using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Net.Http.Json;
using TodoApp.Models;

namespace Todo.Tests
{
    public class TodoTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public TodoTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetTodos_ReturnsEmptyListInitially()
        {
            var todos = await _client.GetFromJsonAsync<List<TodoItem>>("/api/todo");
            Assert.NotNull(todos);
            Assert.Empty(todos);
        }

        [Fact]
        public async Task AddTodo_ReturnsCreatedTodo()
        {
            var newTodo = new TodoItem { Title = "Test Task" };

            var response = await _client.PostAsJsonAsync("/api/todo", newTodo);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var created = await response.Content.ReadFromJsonAsync<TodoItem>();
            Assert.NotNull(created);
            Assert.Equal("Test Task", created.Title);
        }

        [Fact]
        public async Task DeleteTodo_RemovesTodo()
        {
            var todo = new TodoItem { Title = "Delete Me" };
            var createResponse = await _client.PostAsJsonAsync("/api/todo", todo);
            var created = await createResponse.Content.ReadFromJsonAsync<TodoItem>();

            var deleteResponse = await _client.DeleteAsync($"/api/todo/{created!.Id}");
            Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
        }
    }
}