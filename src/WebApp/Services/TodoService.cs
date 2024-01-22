using Client.Abstraction;
using Core.Dto;

namespace WebApp.Services;

public class TodoService(ITodoServiceProxy todoServiceProxy)
{
    public async Task<IEnumerable<TodoHeaderDto>> GetAllAsync()
    {
        var todos = await todoServiceProxy.GetAllAsync();

        return todos;
    }
}