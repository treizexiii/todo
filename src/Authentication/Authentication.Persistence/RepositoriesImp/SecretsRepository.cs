using Authentication.Domain.Entities;
using Authentication.Domain.Repositories;
using Authentication.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Persistence.RepositoriesImp;

public class SecretsRepository(IdentityDb context) : ISecretsRepository
{
    private IQueryable<Secret> _filter = context.Secrets;

    public Task UpdateSecret(Secret secret)
    {
        context.Secrets.Update(secret);
        return Task.CompletedTask;
    }

    public async Task AddSecret(Secret secret)
    {
        await context.Secrets.AddAsync(secret);
    }

    public ISecretsRepository Where(Func<Secret, bool> func)
    {
        _filter = context.Secrets.Where(func).AsQueryable();
        return this;
    }

    public async Task<Secret?> FirstOrDefaultAsync()
    {
        if (_filter is null)
        {
            throw new InvalidOperationException("Filter is not set");
        }
        var result = await _filter.FirstOrDefaultAsync();
        _filter = context.Secrets;

        return result;
    }
}