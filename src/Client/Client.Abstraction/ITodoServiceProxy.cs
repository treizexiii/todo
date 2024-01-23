using Core.Dto;

namespace Client.Abstraction;

public interface ITodoServiceProxy
{
    Task<TodoDto> GetAsync(Guid id);
    Task<IEnumerable<TodoHeaderDto>> GetAllAsync();
    Task<TodoDto> CreateTodoAsync(CreateTodo todo);
    Task<TodoDto> UpdateTodoAsync(UpdateTodo todo);
    Task CompleteTodoAsync(Guid id);

    Task<ItemDto> CreateItemAsync(CreateItem item);
    Task<ItemDto> UpdateItemAsync(UpdateItem item);
    Task CompleteItemAsync(Guid id);
}
