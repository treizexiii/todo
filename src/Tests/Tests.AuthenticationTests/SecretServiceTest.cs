using Authentication.Domain;
using Authentication.Domain.Entities;
using Authentication.Domain.Repositories;
using Authentication.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Tests.AuthenticationTests.Mocks;

namespace Tests.AuthenticationTests;

public class SecretServiceTest
{
    private IServiceProvider _serviceProvider;
    private readonly Guid _owner = new("931cdb5f-42a0-4a2c-b70b-02485c709ff3");

    [SetUp]
    public void Setup()
    {
        var services = new ServiceCollection();
        services.AddAuthenticationDomain();
        services.AddTransient<ISecretsRepository, MockSecretsRepository>();
        _serviceProvider = services.BuildServiceProvider();
    }

    [Test]
    public void ReturnUniqueSecretTest()
    {
        var secretService = _serviceProvider.GetRequiredService<ISecretService>();
        var secret = secretService.GetSecretAsync(_owner, SecretType.Register).Result;
        Assert.That(secret, Is.Not.Null);
    }

    [Test]
    public void TestReturnNullSecretTest()
    {
        var secretService = _serviceProvider.GetRequiredService<ISecretService>();
        var secret = secretService.GetSecretAsync(_owner, "null").Result;
        Assert.That(secret, Is.Null);
    }

    [Test]
    public void ReplaceWithNewSecret()
    {
        var secretService = _serviceProvider.GetRequiredService<ISecretService>();
        var secret = new Secret
        {
            OwnerId = _owner,
            Name = SecretType.Register,
            OpenedAt = DateTime.UtcNow,
            Value = Array.Empty<byte>()
        };

        secretService.CreateSecret(secret).Wait();
        var newSecret = secretService.GetSecretAsync(_owner, SecretType.Register).Result;
        Assert.That(newSecret, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(newSecret.OwnerId, Is.EqualTo(secret.OwnerId));
            Assert.That(newSecret.Name, Is.EqualTo(secret.Name));
            Assert.That(newSecret.OpenedAt, Is.EqualTo(secret.OpenedAt));
        });
    }
}