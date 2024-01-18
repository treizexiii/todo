using Authentication.Domain.Entities.Enums;

namespace Authentication.Domain.Entities;

public class Token
{
    public Guid Id { get; set; }
    public TokenType Type { get; set; } = TokenType.None;
    public byte[] HashToken { get; set; } = Array.Empty<byte>();
    public byte[] SaltToken { get; set; } = Array.Empty<byte>();
    public DateTime CreatedAt { get; set; }
    public DateTime? RevokedAt { get; set; }
    public virtual User User { get; set; } = new();
}