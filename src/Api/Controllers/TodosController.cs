using Core.Dto;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Tools.TransactionManager;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController(
    ILogger<TodosController> logger,
    ITransactionManager transaction,
    ITodoService todoService,
    IItemService itemService)
    : BaseController(logger, transaction)
{
    private readonly Guid _userId = Guid.NewGuid();

    [HttpGet]
    public async Task<IActionResult> GetTodos()
    {
        Logger.LogInformation("GetTodos");
        try
        {
            await Transaction.BeginTransactionAsync(_userId);
            var todos = await todoService.GetAllAsync(new TodoFilterOptions
            {
                IsCompleted = false
            });
            await Transaction.CommitTransactionAsync(_userId);
            return Ok(todos.Select(x => x.ToDto()));
        }
        catch (Exception e)
        {
            await Transaction.RollbackTransactionAsync(_userId, e);
            Logger.LogError(e, "Error getting todos");
            return Error(e);
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTodo(Guid id)
    {
        Logger.LogInformation("GetTodo");
        try
        {
            await Transaction.BeginTransactionAsync(_userId);
            var todo = await todoService.GetAsync(id);
            await Transaction.CommitTransactionAsync(_userId);
            return Ok(todo?.ToDto());
        }
        catch (Exception e)
        {
            await Transaction.RollbackTransactionAsync(_userId, e);
            Logger.LogError(e, "Error getting todo");
            return Error(e);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodo(CreateTodo request)
    {
        Logger.LogInformation("CreateTodo");
        try
        {
            await Transaction.BeginTransactionAsync(_userId);
            var todo = await todoService.CreateAsync(request);
            await Transaction.CommitTransactionAsync(_userId);
            return Ok(todo.ToDto());
        }
        catch (Exception e)
        {
            await Transaction.RollbackTransactionAsync(_userId, e);
            Logger.LogError(e, "Error creating todo");
            return Error(e);
        }
    }

    [HttpPost("{id:guid}")]
    public async Task<IActionResult> UpdateTodo(Guid id, UpdateTodo request)
    {
        Logger.LogInformation("UpdateTodo");
        try
        {
            await Transaction.BeginTransactionAsync(_userId);
            var todo = await todoService.UpdateAsync(id, request);
            await Transaction.CommitTransactionAsync(_userId);
            return Ok(todo.ToDto());
        }
        catch (Exception e)
        {
            await Transaction.RollbackTransactionAsync(_userId, e);
            Logger.LogError(e, "Error updating todo");
            return Error(e);
        }
    }

    [HttpPut("{id:guid}/complete")]
    public async Task<IActionResult> CompleteTodo(Guid id)
    {
        Logger.LogInformation("CompleteTodo");
        try
        {
            await Transaction.BeginTransactionAsync(_userId);
            var todo = await todoService.CompleteAsync(id);
            await Transaction.CommitTransactionAsync(_userId);
            return Ok(todo.ToDto());
        }
        catch (Exception e)
        {
            await Transaction.RollbackTransactionAsync(_userId, e);
            Logger.LogError(e, "Error completing todo");
            return Error(e);
        }
    }

    // [HttpGet("{id:guid}/suggestions")]
    // public async Task<IActionResult> GetTodoSuggestions(Guid id)
    // {
    //     Logger.LogInformation("GetTodoSuggestions");
    //     try
    //     {
    //         await Transaction.BeginTransactionAsync(_userId);
    //         var todo = await todoService.GetAsync(id);
    //         if (todo is null)
    //         {
    //             throw new FileNotFoundException("Todo not found");
    //         }
    //         var suggestions = await todoService.GetSuggestionsAsync(todo.TodoType);
    //         await Transaction.CommitTransactionAsync(_userId);
    //         return Ok(todo.ToDto());
    //     }
    //     catch (Exception e)
    //     {
    //         await Transaction.RollbackTransactionAsync(_userId, e);
    //         Logger.LogError(e, "Error getting todo suggestions");
    //         return Error(e);
    //     }
    // }

    [HttpPost("{id:guid}/items")]
    public async Task<IActionResult> CreateItem(Guid id, CreateItem request)
    {
        Logger.LogInformation("CreateItem");
        try
        {
            await Transaction.BeginTransactionAsync(_userId);
            var item = await itemService.CreateAsync(id, request);
            await Transaction.CommitTransactionAsync(_userId);
            return Ok(item.ToDto());
        }
        catch (Exception e)
        {
            await Transaction.RollbackTransactionAsync(_userId, e);
            Logger.LogError(e, "Error creating item");
            return Error(e);
        }
    }

    [HttpPost("{id:guid}/items/{itemId:guid}")]
    public async Task<IActionResult> UpdateItem(Guid id, Guid itemId, UpdateItem request)
    {
        Logger.LogInformation("UpdateItem");
        try
        {
            await Transaction.BeginTransactionAsync(_userId);
            var item = await itemService.UpdateAsync(itemId, request);
            await Transaction.CommitTransactionAsync(_userId);
            return Ok(item.ToDto());
        }
        catch (Exception e)
        {
            await Transaction.RollbackTransactionAsync(_userId, e);
            Logger.LogError(e, "Error updating item");
            return Error(e);
        }
    }

    [HttpPut("{id:guid}/items/{itemId:guid}/complete")]
    public async Task<IActionResult> CompleteItem(Guid id, Guid itemId)
    {
        Logger.LogInformation("CompleteItem");
        try
        {
            await Transaction.BeginTransactionAsync(_userId);
            var item = await itemService.CompleteAsync(itemId);
            await Transaction.CommitTransactionAsync(_userId);
            return Ok(item.ToDto());
        }
        catch (Exception e)
        {
            await Transaction.RollbackTransactionAsync(_userId, e);
            Logger.LogError(e, "Error completing item");
            return Error(e);
        }
    }

    [HttpDelete("{id:guid}/items/{itemId:guid}")]
    public async Task<IActionResult> DeleteItem(Guid id, Guid itemId)
    {
        Logger.LogInformation("DeleteItem");
        try
        {
            await Transaction.BeginTransactionAsync(_userId);
            await itemService.DeleteAsync(itemId);
            await Transaction.CommitTransactionAsync(_userId);
            return Ok("Removed");
        }
        catch (Exception e)
        {
            await Transaction.RollbackTransactionAsync(_userId, e);
            Logger.LogError(e, "Error deleting item");
            return Error(e);
        }
    }                                                                                                                                           
}