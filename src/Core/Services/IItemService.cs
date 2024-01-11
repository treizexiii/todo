using Core.Dto;
using Core.Entities;

namespace Core.Services;

public interface IItemService
{
    Task<IEnumerable<Item>> GetAllAsync(Guid todoId);
    Task<Item?> GetAsync(Guid id);
    Task<Item> CreateAsync(Guid todoId, CreateItem item);
    Task<Item> UpdateAsync(Guid id, UpdateItem item);
    Task<Item> CompleteAsync(Guid id);
}