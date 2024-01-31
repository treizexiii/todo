using Client.Abstraction;
using Core.Dto;

namespace WebApp.Services;

public class TodoService(ITodoServiceProxy todoServiceProxy)
{
    public TodoDto? _todo { get; set; } = null;

    public async Task<IEnumerable<TodoHeaderDto>> GetAllAsync()
    {
        var todos = await todoServiceProxy.GetAllAsync();

        return todos;
    }

    public async Task<TodoDto> GetAsync(Guid id)
    {
        var todo = await todoServiceProxy.GetAsync(id);
        _todo = todo;
        return _todo;
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
}