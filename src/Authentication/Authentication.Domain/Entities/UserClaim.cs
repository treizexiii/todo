namespace Authentication.Domain.Entities;

public class UserClaim
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; } = new();
    public Guid ClaimId { get; set; }
    public virtual Claim Claim { get; set; } = new();
}