using System.Net.Http.Json;
using Client.Abstraction;
using Core.Dto;

namespace Client.HttpRestClient;

public class TodoHttpRestClient(HttpClient httpClient) : ITodoServiceProxy, IDisposable
{
    public async Task<TodoDto> GetAsync(Guid id)
    {
        var response = await httpClient.GetAsync($"api/todo/{id}");
        response.EnsureSuccessStatusCode();
        var todo = await response.Content.ReadFromJsonAsync<Response<TodoDto>>();
        return todo.Data!;
    }

    public async Task<IEnumerable<TodoHeaderDto>> GetAllAsync()
    {
        var response = await httpClient.GetAsync("api/todo");
        response.EnsureSuccessStatusCode();
        var todos = await response.Content.ReadFromJsonAsync<Response<IEnumerable<TodoHeaderDto>>>();
        return todos.Data!;
    }

    public async Task<TodoDto> CreateTodoAsync(CreateTodo todo)
    {
        var response = await httpClient.PostAsJsonAsync("api/todo", todo);
        response.EnsureSuccessStatusCode();
        var createdTodo = await response.Content.ReadFromJsonAsync<Response<TodoDto>>();
        return createdTodo.Data!;
    }

    public async Task<TodoDto> UpdateTodoAsync(UpdateTodo todo)
    {
        var response = await httpClient.PutAsJsonAsync($"api/todo/{todo.Id}", todo);
        response.EnsureSuccessStatusCode();
        var updatedTodo = await response.Content.ReadFromJsonAsync<Response<TodoDto>>();
        return updatedTodo.Data!;
    }

    public async Task<ItemDto> CreateAsync(CreateItem item)
    {
        var response = await httpClient.PostAsJsonAsync($"api/todo/{item.TodoId}/items", item);
        response.EnsureSuccessStatusCode();
        var createdItem = await response.Content.ReadFromJsonAsync<Response<ItemDto>>();
        return createdItem.Data!;
    }

    public async Task<ItemDto> UpdateAsync(UpdateItem item)
    {
        var response = await httpClient.PutAsJsonAsync($"api/todo/{item.TodoId}/items/{item.Id}", item);
        response.EnsureSuccessStatusCode();
        var updatedItem = await response.Content.ReadFromJsonAsync<Response<ItemDto>>();
        return updatedItem.Data!;
    }

    public async Task CompleteTodoAsync(Guid id)
    {
        var response = await httpClient.PutAsync($"api/todo/{id}/complete", null!);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<Response>();
    }

    public Task<ItemDto> CreateItemAsync(CreateItem item)
    {
        throw new NotImplementedException();
    }

    public Task<ItemDto> UpdateItemAsync(UpdateItem item)
    {
        throw new NotImplementedException();
    }

    public Task CompleteItemAsync(Guid id)
    {
        throw new NotImplementedException();
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
            httpClient.Dispose();
        }
    }
}