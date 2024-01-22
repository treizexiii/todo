using Core.Dto;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using TodoClient.Interfaces;

namespace TodoClient.Implementations;

public class TodoServiceGrpcClient(string url) : ITodoServiceProxy, IDisposable
{
    private readonly GrpcChannel _channel = GrpcChannel.ForAddress(url, new GrpcChannelOptions {
        HttpHandler = new GrpcWebHandler(new HttpClientHandler())
    });

    public Task<IEnumerable<TodoHeaderDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TodoDto> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<TodoDto> CreateAsync(CreateTodo todo)
    {
        throw new NotImplementedException();
    }

    public Task<TodoDto> UpdateAsync(UpdateTodo todo)
    {
        throw new NotImplementedException();
    }

    public Task<TodoHeaderDto> CompleteAsync(Guid id)
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
            _channel.Dispose();
        }
    }
}