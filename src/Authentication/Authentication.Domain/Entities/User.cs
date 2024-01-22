using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
    public bool IsActivated { get; set; }
    public DateTime? ActivatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    // public Guid RoleId { get; set; }
    public virtual Role Role { get; set; } = new();
    // not mapping to db
    [NotMapped] public Person Person { get; set; } = new();
    public virtual List<UserClaim> UserClaims { get; set; } = new();
    public virtual List<Token> Tokens { get; set; } = new();
}

public class Person
{
    public Guid Id { get; set; }
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}


// private static bool AlreadyExists(User? user, Claim? claim)
// {
//     if (user == null || claim == null)
//     {
//         return true;
//     }
//
//     if (user.UserClaims.Any(x => x.ClaimId == claim.Id))
//     {
//         return true;
//     }
//
//     return false;
// }