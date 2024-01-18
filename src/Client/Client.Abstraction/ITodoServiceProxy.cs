using Core.Dto;

namespace Client.Abstraction;

public interface ITodoServiceProxy
{
    Task<TodoDto> GetAsync(Guid id);
    Task<IEnumerable<TodoHeaderDto>> GetAllAsync();
    Task<TodoDto> CreateAsync(CreateTodo todo);
    Task<TodoDto> UpdateAsync(UpdateTodo todo);
    Task CompleteAsync(Guid id);
}
