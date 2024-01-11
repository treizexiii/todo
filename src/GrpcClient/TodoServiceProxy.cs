using Grpc.Net.Client;
using GrpcMessage;
using TodoService = GrpcMessage.TodoService;

namespace GrpcClient;

public class TodoServiceProxy : IDisposable
{
    private readonly GrpcChannel _channel;

    public TodoServiceProxy(string url)
    {
        _channel = GrpcChannel.ForAddress(url);
    }

    public async Task<IEnumerable<TodoMessage>> GetAllAsync()
    {
        var client = new TodoService.TodoServiceClient(_channel);
        var response = await client.GetTodosAsync(new GetTodosRequest
        {
            IsCompleted = false
        });

        var todos = response.Datas.Select(data => data.Unpack<TodoMessage>());
        return todos;
    }

    public async Task<TodoMessage> CreateAsync(CreateTodoMessage todo)
    {
        var client = new TodoService.TodoServiceClient(_channel);
        var response = await client.CreateTodoAsync(todo);
        return response.Data.Unpack<TodoMessage>();
    }

    public async Task<TodoMessage> UpdateAsync(UpdateTodoMessage todo)
    {
        var client = new TodoService.TodoServiceClient(_channel);
        var response = await client.UpdateTodoAsync(todo);
        return response.Data.Unpack<TodoMessage>();
    }

    public async Task<TodoMessage> CompleteAsync(Guid id)
    {
        var client = new TodoService.TodoServiceClient(_channel);
        var response = await client.CompleteTodoAsync(new CompleteTodoMessage
        {
            Id = id.ToString()
        });
        return response.Data.Unpack<TodoMessage>();
    }

    public void Dispose()
    {
        _channel.Dispose();
    }
}