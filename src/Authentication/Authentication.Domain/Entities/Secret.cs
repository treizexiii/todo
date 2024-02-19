namespace Authentication.Domain.Entities;

public class Secret
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTimeOffset OpenedAt { get; set; } = DateTime.UtcNow;
    public DateTimeOffset? ClosedAt { get; set; }
    public DateTimeOffset? RevokedAt { get; set; }
    public byte[] Value { get; set; } = Array.Empty<byte>();
    public byte[]? Salt { get; set; }
}

public static class SecretType
{
    public const string Register = "Register";
    public const string ResetPassword = "ResetPassword";
    public const string Password = "Password";
}