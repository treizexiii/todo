using System.Net.Http.Json;
using Core.Dto;
using TodoClient.Interfaces;
using TodoClient.Wrapper;

namespace TodoClient.Implementations;

public class TodoServiceHttpClient(HttpClient client) : IDisposable, ITodoServiceProxy
{
    public async Task<IEnumerable<TodoHeaderDto>> GetAllAsync()
    {
        var response = await client.GetAsync("/api/todos");
        response.EnsureSuccessStatusCode();
        var todos = await response.Content.ReadFromJsonAsync<Response<IEnumerable<TodoHeaderDto>>>();
        if (todos is null)
        {
            throw new Exception("Failed to deserialize todos");
        }
        return todos.Data;
    }

    public async Task<TodoDto> GetAsync(Guid id)
    {
        var response = await client.GetAsync($"/api/todos/{id}");
        response.EnsureSuccessStatusCode();
        var todo = await response.Content.ReadFromJsonAsync<Response<TodoDto>>();
        if (todo is null)
        {
            throw new Exception("Failed to deserialize todo");
        }
        return todo.Data;
    }

    public async Task<TodoDto> CreateAsync(CreateTodo todo)
    {
        var response = await client.PostAsJsonAsync("/api/todos", todo);
        response.EnsureSuccessStatusCode();
        var createdTodo = await response.Content.ReadFromJsonAsync<Response<TodoDto>>();
        if (createdTodo is null)
        {
            throw new Exception("Failed to deserialize todo");
        }
        return createdTodo.Data;
    }

    public async Task<TodoDto> UpdateAsync(UpdateTodo todo)
    {
        var response = await client.PutAsJsonAsync($"/api/todos/{todo.Id}", todo);
        response.EnsureSuccessStatusCode();
        var updatedTodo = await response.Content.ReadFromJsonAsync<Response<TodoDto>>();
        if (updatedTodo is null)
        {
            throw new Exception("Failed to deserialize todo");
        }
        return updatedTodo.Data;
    }

    public async Task<TodoHeaderDto> CompleteAsync(Guid id)
    {
        var response = await client.PostAsJsonAsync($"/api/todos/{id}/complete", new { });
        response.EnsureSuccessStatusCode();
        var completedTodo = await response.Content.ReadFromJsonAsync<Response<TodoHeaderDto>>();
        if (completedTodo is null)
        {
            throw new Exception("Failed to deserialize todo");
        }
        return completedTodo.Data;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            client.Dispose();
        }
    }
}