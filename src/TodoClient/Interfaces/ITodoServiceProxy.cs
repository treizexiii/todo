using Core.Dto;

namespace TodoClient.Interfaces;

public interface ITodoServiceProxy
{
    Task<IEnumerable<TodoHeaderDto>> GetAllAsync();
    Task<TodoDto> GetAsync(Guid id);
    Task<TodoDto> CreateAsync(CreateTodo todo);
    Task<TodoDto> UpdateAsync(UpdateTodo todo);
    Task<TodoHeaderDto> CompleteAsync(Guid id);
}