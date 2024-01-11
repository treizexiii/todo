using Core.Services;
using Grpc.Core;
using GrpcMessage;
using GrpcMessage.Item;

namespace GrpcService.Services;

public class ItemGRpcService(ILogger<ItemGRpcService> logger, IItemService service)
    : GrpcMessage.Item.ItemService.ItemServiceBase
{
    public override async Task<Response> GetItems(GetItemsMessage request, ServerCallContext context)
    {
        logger.LogInformation("Getting items");
        try
        {
            var items = await service.GetAllAsync(Guid.Parse(request.TodoId));
            var messages = items.Select(t => t.CreateItemMessage());
            return ResponseFactory.CreateSuccessResponse(messages);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error getting items");
            return ResponseFactory.CreateErrorResponse(500, e.Message);
        }
    }

    public override async Task<Response> AddItem(AddItemMessage request, ServerCallContext context)
    {
        logger.LogInformation("Adding item");
        try
        {
            var dto = request.ToCreateItemMessage();
            var item = await service.CreateAsync(Guid.Parse(request.TodoId), dto);

            return ResponseFactory.CreateSuccessResponse(item.CreateItemMessage());
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error adding item");
            return ResponseFactory.CreateErrorResponse(500, e.Message);
        }
    }

    public override async Task<Response> UpdateItem(UpdateItemMessage request, ServerCallContext context)
    {
        logger.LogInformation("Updating item");
        try
        {
            var item = await service.UpdateAsync(
                Guid.Parse(request.Id),
                request.ToUpdateItemMessage());
            return ResponseFactory.CreateSuccessResponse(item.CreateItemMessage());
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error updating item");
            return ResponseFactory.CreateErrorResponse(500, e.Message);
        }
    }

    public override async Task<Response> CompleteItem(CompleteItemMessage request, ServerCallContext context)
    {
        logger.LogInformation("Completing item");
        try
        {
            var item = await service.CompleteAsync(Guid.Parse(request.Id));
            return ResponseFactory.CreateSuccessResponse(item.CreateItemMessage());
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error completing item");
            return ResponseFactory.CreateErrorResponse(500, e.Message);
        }
    }
}