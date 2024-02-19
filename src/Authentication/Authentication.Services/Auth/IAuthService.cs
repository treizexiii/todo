using Authentication.Domain.Entities;
using Authentication.Domain.Entities.Enums;
using Authentication.Domain.Repositories;
using Authentication.Domain.Services;
using Authentication.Services.Configuration;
using Authentication.Tools;
using Authentication.Wrappers;

namespace Authentication.Services.Auth;

public interface IAuthService
{
    Task<User> LoginAsync(LoginDto request);
    Task<(string jwt, string refresh)> ProduceJwtToken(User user);
    Task<User> ControlTokenAsync(string? refreshToken);

    Task Register(RegisterDto registerAdminModel, Guid objectId);
    // Task<UserSession?> GetSessionAsync(Guid sessionId);
    // Task StoreSessionAsync(UserSession session);
    // Task RevokeSessionAsync(Guid id);
}

public interface ITokenManager
{
    Task<string> CreateRegisterTokenAsync(RoleEnum role, string discriminator, Guid objectId, string key);
    Task<(Guid ObjectId, string token)> RegenerateRegisterTokenAsync(RoleEnum role, string discriminator, string key);
    Task<(bool success, Guid? objectId)> ConsumerRegisterTokenAsync(string token, string name, string key);
}

public class AuthenticationService(
    AuthOptions configuration,
    ISecretService secretService,
    IUsersRepository usersRepository)
    : IAuthService, ITokenManager
{
    public Task<User> LoginAsync(LoginDto request)
    {
        throw new NotImplementedException();
    }

    public Task<(string jwt, string refresh)> ProduceJwtToken(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User> ControlTokenAsync(string? refreshToken)
    {
        throw new NotImplementedException();
    }

    public Task Register(RegisterDto registerAdminModel, Guid objectId)
    {
        throw new NotImplementedException();
    }

    public async Task<string> CreateRegisterTokenAsync(RoleEnum role, string discriminator, Guid objectId, string key)
    {
        var utc = DateTime.UtcNow;
        var token = DataHasher.CreateRandomString(8);
        var hash = DataHasher.Hash(token, key);
        var secret = new Secret();
        secret.Id = Guid.NewGuid();
        secret.OwnerId = objectId;
        secret.Name = role + ":" + discriminator;
        secret.OpenedAt = utc;
        secret.Value = hash.hash;
        secret.RevokedAt = utc.AddDays(1);

#if DEBUG
        Console.WriteLine($"Token: {discriminator} {token}");
#endif

        await secretService.CreateSecret(secret);

        return token;
    }

    public Task<(Guid ObjectId, string token)> RegenerateRegisterTokenAsync(RoleEnum role, string discriminator,
        string key)
    {
        throw new NotImplementedException();
    }

    public Task<(bool success, Guid? objectId)> ConsumerRegisterTokenAsync(string token, string name, string key)
    {
        throw new NotImplementedException();
    }
}