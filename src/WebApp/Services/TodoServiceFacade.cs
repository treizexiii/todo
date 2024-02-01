using Client.Abstraction;
using Core.Dto;

namespace WebApp.Services;

public class TodoServiceFacade(ITodoServiceProxy todoServiceProxy)
{
    public async Task<IEnumerable<TodoHeaderDto>> GetAllAsync()
    {
        var todos = await todoServiceProxy.GetAllAsync();

        return todos;
    }

    public async Task<TodoDto> GetAsync(Guid id)
    {
        return await todoServiceProxy.GetAsync(id);
    }

    public async Task<TodoDto> CreateAsync(CreateTodo todo)
    {
        var createdTodo = await todoServiceProxy.CreateTodoAsync(todo);

        return createdTodo;
    }

    public async Task<TodoDto> UpdateAsync(UpdateTodo todo)
    {
        var updatedTodo = await todoServiceProxy.UpdateTodoAsync(todo);

        return updatedTodo;
    }

    public async Task<ItemDto> CreateItemAsync(CreateItem item)
    {
        var createdItem = await todoServiceProxy.CreateItemAsync(item);

        return createdItem;
    }

    public async Task<ItemDto> UpdateItemAsync(UpdateItem item)
    {
        var updatedItem = await todoServiceProxy.UpdateItemAsync(item);

        return updatedItem;
    }

    public async Task CompleteItemAsync(Guid todoId, Guid itemId)
    {
        await todoServiceProxy.CompleteItemAsync(todoId, itemId);
    }
}