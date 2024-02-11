using Core.Dto;
using Core.Entities;
using Core.Repositories;

namespace Core.Services;

public class ItemService(IItemsRepository repository) : IItemService
{
    public async Task<IEnumerable<Item>> GetAllAsync(Guid todoId)
    {
        return await repository.GetAllAsync(todoId);
    }

    public async Task<Item?> GetAsync(Guid id)
    {
        return await repository.GetAsync(id);
    }

    public async Task<Item> CreateAsync(Guid todoId, CreateItem item)
    {
        if (!await repository.ControlListAsync(todoId)) throw new Exception("Todo not found");

        var utc = DateTime.UtcNow;
        var itemEntity = new Item
        {
            Id = Guid.NewGuid(),
            Title = item.Title,
            Description = item.Description ?? string.Empty,
            DueDate = item.DueDate,
            IsCompleted = false,
            CreatedAt = utc,
            UpdatedAt = utc,
            TodoId = todoId
        };

        await repository.CreateAsync(itemEntity);

        return itemEntity;
    }

    public async Task<Item> UpdateAsync(Guid id, UpdateItem item)
    {
        var itemEntity = await repository.GetAsync(id);
        if (itemEntity is null) throw new FileNotFoundException($"Item with id {id} not found");

        itemEntity.Title = item.Title;
        itemEntity.Description = item.Description ?? string.Empty;
        itemEntity.DueDate = item.DueDate;

        itemEntity.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(itemEntity);

        return itemEntity;
    }

    public async Task<Item> CompleteAsync(Guid id)
    {
        var itemEntity = await repository.GetAsync(id);
        if (itemEntity is null) throw new FileNotFoundException($"Item with id {id} not found");

        itemEntity.IsCompleted = !itemEntity.IsCompleted;

        itemEntity.CompletedAt = itemEntity.IsCompleted ?
            DateTime.UtcNow :
            null;
        itemEntity.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(itemEntity);

        return itemEntity;
    }

    public async Task DeleteAsync(Guid id)
    {
        await repository.DeleteAsync(id);
    }
}