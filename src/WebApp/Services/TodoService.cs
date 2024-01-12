using GrpcClient;
using GrpcMessage.Todo;

namespace WebApp.Services;

public class TodoService(TodoServiceProxy todoServiceProxy, ItemServiceProxy itemServiceProxy)
{
    public async Task<IEnumerable<TodoListMessage>> GetAllAsync()
    {
        var todos = await todoServiceProxy.GetAllAsync();

        return todos;
    }
}