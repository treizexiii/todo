using Core.Dto;
using Core.Repositories;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Tools.TransactionManager;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController(ILogger<TodoController> logger, ITransactionManager transaction, ITodoService todoService)
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
            return Ok(todos);
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
            return Ok(todo);
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
            return Ok(todo);
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
            return Ok(todo);
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
            return Ok(todo);
        }
        catch (Exception e)
        {
            await Transaction.RollbackTransactionAsync(_userId, e);
            Logger.LogError(e, "Error completing todo");
            return Error(e);
        }
    }
}