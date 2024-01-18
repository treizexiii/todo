using Authentication.Domain.Entities.Enums;

namespace Authentication.Domain.Entities;

public class Claim
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public ClaimType Type { get; set; } = ClaimType.None;
    public virtual List<UserClaim> UserClaims { get; set; } = [];
}