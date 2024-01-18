using Core.Dto;

namespace Client.Abstraction;

public interface IItemServiceProxy
{
    Task<ItemDto> CreateAsync(CreateItem item);
    Task<ItemDto> UpdateAsync(UpdateItem item);
    Task CompleteAsync(Guid id);
}