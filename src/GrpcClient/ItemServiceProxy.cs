using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using GrpcMessage.Item;

namespace GrpcClient;

public class ItemServiceProxy : IDisposable
{
    private readonly GrpcChannel _channel;

    public ItemServiceProxy(string url)
    {
        _channel = GrpcChannel.ForAddress(url, new GrpcChannelOptions {
            HttpHandler = new GrpcWebHandler(new HttpClientHandler())
        });
    }

    public async Task<ItemMessage> CreateAsync(AddItemMessage item)
    {
        var client = new ItemService.ItemServiceClient(_channel);
        var response = await client.AddItemAsync(item);
        return response.Data.Unpack<ItemMessage>();
    }

    public async Task<ItemMessage> UpdateAsync(UpdateItemMessage item)
    {
        var client = new ItemService.ItemServiceClient(_channel);
        var response = await client.UpdateItemAsync(item);
        return response.Data.Unpack<ItemMessage>();
    }

    public async Task<ItemMessage> CompleteAsync(Guid id)
    {
        var client = new ItemService.ItemServiceClient(_channel);
        var response = await client.CompleteItemAsync(new CompleteItemMessage
        {
            Id = id.ToString()
        });
        return response.Data.Unpack<ItemMessage>();
    }

    public void Dispose()
    {
        _channel.Dispose();
    }
}