using Authentication.Domain.Entities;
using Authentication.Domain.Repositories;

namespace Tests.AuthenticationTests.Mocks;

public class MockSecretsRepository : ISecretsRepository
{
    private IQueryable<Secret> _filter;
    private readonly SecretList _secrets;

    public MockSecretsRepository()
    {
        _secrets = new SecretList();
        _filter = _secrets.AsQueryable();
    }

    public Task UpdateSecret(Secret secret)
    {
        var index = _secrets.FindIndex(x => x.Id == secret.Id);
        if (index >= 0)
        {
            _secrets[index] = secret;
        }
        return Task.CompletedTask;
    }

    public Task AddSecret(Secret secret)
    {
        _secrets.Add(secret);
        return Task.CompletedTask;
    }

    public ISecretsRepository Where(Func<Secret, bool> func)
    {
        _filter = _secrets.Where(func).AsQueryable();
        return this;
    }

    public Task<Secret?> FirstOrDefaultAsync()
    {
        var result = _filter.FirstOrDefault();
        _filter = _secrets.AsQueryable();

        return Task.FromResult(result);
    }
}

public class SecretList : List<Secret>
{
    public SecretList()
    {
        Add(new Secret
        {
            Id = Guid.Parse("c5fa0761-10a3-47cc-904a-e394a1ebc524"),
            OwnerId = Guid.Parse("931cdb5f-42a0-4a2c-b70b-02485c709ff3"),
            Name = SecretType.Register,
            OpenedAt = new DateTime(2021, 1, 6),
            RevokedAt = DateTime.Now.AddDays(1),
            Value = Array.Empty<byte>()
        });
        Add(new Secret
        {
            Id = Guid.Parse("66f3f7e9-2079-4284-a49c-e25c9e5882ef"),
            OwnerId = Guid.Parse("931cdb5f-42a0-4a2c-b70b-02485c709ff3"),
            Name = SecretType.Register,
            OpenedAt = new DateTime(2021, 1, 1),
            ClosedAt = new DateTime(2021, 1, 2),
            Value = Array.Empty<byte>()
        });
        Add(new Secret
        {
            Id = Guid.Parse("251fc103-6f52-4b73-911e-78f50a22510e"),
            OwnerId = Guid.Parse("931cdb5f-42a0-4a2c-b70b-02485c709ff3"),
            Name = SecretType.Register,
            OpenedAt = new DateTime(2021, 1, 2),
            ClosedAt = new DateTime(2021, 1, 3),
            Value = Array.Empty<byte>()
        });
        Add(new Secret
        {
            Id = Guid.Parse("47c3b7f2-ee6f-4089-87f6-fafb2b15065b"),
            OwnerId = Guid.Parse("931cdb5f-42a0-4a2c-b70b-02485c709ff3"),
            Name = SecretType.Register,
            OpenedAt = new DateTime(2021, 1, 3),
            ClosedAt = new DateTime(2021, 1, 4),
            Value = Array.Empty<byte>()
        });
        Add(new Secret
        {
            Id = Guid.Parse("23f613b3-665d-45a3-ab32-91801caacd90"),
            OwnerId = Guid.Parse("931cdb5f-42a0-4a2c-b70b-02485c709ff3"),
            Name = SecretType.Register,
            OpenedAt = new DateTime(2021, 1, 4),
            ClosedAt = new DateTime(2021, 1, 5),
            RevokedAt = new DateTime(2021, 1, 5),
            Value = Array.Empty<byte>()
        });
    }
}
