using Authentication.Domain.Entities;
using Authentication.Domain.Repositories;

namespace Authentication.Domain.Services;

public interface ISecretService
{
    Task<Secret?> GetSecretAsync(Guid owner, string name);
    Task CreateSecret(Secret secret);
    Task RevokeSecret(Guid owner, string name);
    Task ConsumeSecret(Guid owner, string name);
}

internal class SecretService(ISecretsRepository secretsRepository) : ISecretService
{
    public async Task<Secret?> GetSecretAsync(Guid owner, string name)
    {
        var utc = DateTime.UtcNow;
        return await secretsRepository
            .Where(x => x.OwnerId == owner &&
                        x.Name == name &&
                        (x.RevokedAt is null || x.RevokedAt > utc) &&
                        x.ClosedAt is null)
            .FirstOrDefaultAsync();
    }

    public async Task CreateSecret(Secret secret)
    {
        var oldSecret = await GetSecretAsync(secret.OwnerId, secret.Name);
        if (oldSecret is not null)
        {
            oldSecret.ClosedAt = DateTime.UtcNow;
            await secretsRepository.UpdateSecret(oldSecret);
        }

        await secretsRepository.AddSecret(secret);
    }

    public async Task RevokeSecret(Guid owner, string name)
    {
        var utc = DateTime.UtcNow;
        var secret = await GetSecretAsync(owner, name);
        if (secret is not null)
        {
            secret.RevokedAt = utc;
            secret.ClosedAt = utc;
            await secretsRepository.UpdateSecret(secret);
        }
    }

    public async Task ConsumeSecret(Guid owner, string name)
    {
        var utc = DateTime.UtcNow;
        var secret = await GetSecretAsync(owner, name);
        if (secret is not null)
        {
            secret.ClosedAt = utc;
            await secretsRepository.UpdateSecret(secret);
        }
    }
}