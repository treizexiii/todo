using Authentication.Domain.Entities;

namespace Authentication.Domain.Repositories;

public interface ISecretsRepository
{
    Task UpdateSecret(Secret secret);
    Task AddSecret(Secret secret);
    ISecretsRepository Where(Func<Secret, bool> func);
    Task<Secret?> FirstOrDefaultAsync();
}