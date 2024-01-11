using Core.Entities;

namespace Core.Repositories;

public interface IItemsRepository
{
    Task<IEnumerable<Item>> GetAllAsync(Guid todoId);
    Task<Item?> GetAsync(Guid itemId);
    Task CreateAsync(Item item);
    Task UpdateAsync(Item item);
    Task DeleteAsync(Guid itemId);
    Task<bool> ControlListAsync(Guid todoId);
}