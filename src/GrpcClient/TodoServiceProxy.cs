using Grpc.Net.Client;
using GrpcMessage.Todo;
using TodoService = GrpcMessage.Todo.TodoService;

namespace GrpcClient;

public class TodoServiceProxy : IDisposable
{
    private readonly GrpcChannel _channel;

    public TodoServiceProxy(string url)
    {
        _channel = GrpcChannel.ForAddress(url);
    }

    public async Task<IEnumerable<TodoListMessage>> GetAllAsync()
    {
        var client = new TodoService.TodoServiceClient(_channel);
        var response = await client.GetTodosAsync(new TodosFilterMessage
        {
            IsCompleted = false
        });

        var todos = response.Datas.Select(data => data.Unpack<TodoListMessage>());
        return todos;
    }

    public async Task<TodoListMessage> CreateAsync(CreateTodoMessage todo)
    {
        var client = new TodoService.TodoServiceClient(_channel);
        var response = await client.CreateTodoAsync(todo);
        return response.Data.Unpack<TodoListMessage>();
    }

    public async Task<TodoListMessage> UpdateAsync(UpdateTodoMessage todo)
    {
        var client = new TodoService.TodoServiceClient(_channel);
        var response = await client.UpdateTodoAsync(todo);
        return response.Data.Unpack<TodoListMessage>();
    }

    public async Task<TodoListMessage> CompleteAsync(Guid id)
    {
        var client = new TodoService.TodoServiceClient(_channel);
        var response = await client.CompleteTodoAsync(new CompleteTodoMessage
        {
            Id = id.ToString()
        });
        return response.Data.Unpack<TodoListMessage>();
    }

    public void Dispose()
    {
        _channel.Dispose();
    }
}