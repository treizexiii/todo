using Authentication.Persistence.Database;
using Authentication.Services.Admin;
using Authentication.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Tools.TransactionManager;

namespace Authentication.IdentityServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController(ITransactionManager transactionManager, IAdminUser admin) : ControllerBase
{
    private Guid _user = Guid.NewGuid();

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        try
        {
            await transactionManager.BeginTransactionAsync(_user, typeof(IdentityDb));
            var users = await admin.GetUsers();
            await transactionManager.CommitTransactionAsync(_user, typeof(IdentityDb));
            return Ok(users);
        }
        catch (Exception e)
        {
            await transactionManager.RollbackTransactionAsync(_user, e, typeof(IdentityDb));
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser()
    {
        try
        {
            await transactionManager.BeginTransactionAsync(_user, typeof(IdentityDb));
            var user = new NewUserDto(Guid.NewGuid(), "test2", "test2@test.fr");
            var result = await admin.AddUser(user);

            await transactionManager.CommitTransactionAsync(_user, typeof(IdentityDb));
            return Ok(result);
        }
        catch (Exception e)
        {
            await transactionManager.RollbackTransactionAsync(_user, e, typeof(IdentityDb));
            return BadRequest(e.Message);
        }
    }
}