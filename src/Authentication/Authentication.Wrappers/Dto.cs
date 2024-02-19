using Authentication.Domain.Entities;
using Authentication.Domain.Entities.Enums;

namespace Authentication.Wrappers;

public record LoginDto(string Username, string Password);

public record RegisterDto(string Username, string Password);

public record NewUserDto(Guid? Id, string Email, string ConfirmEmail, string Password, string ConfirmPassword)
{
    public bool IsValid()
    {
        return Email == ConfirmEmail || Password == ConfirmPassword;
    }
}

public record NewClaimDto(Guid Guid, string Name, string Value, ClaimType Type);

public record ClaimDto(Guid Id, string Name, string Value)
{
    public static ClaimDto GetClaimDto(Claim claim)
    {
        return new ClaimDto(claim.Id, claim.Name, claim.Value);
    }
}

public record UserDto(Guid Id, string Username, string Email, bool IsActivated, DateTime? ActivatedAt, bool IsDeleted,
    DateTime? DeletedAt, IEnumerable<ClaimDto> Claims, string Role)
{
    public static UserDto GetUserDto(User user, IEnumerable<ClaimDto> claims)
    {
        return new UserDto(user.Id, user.Username, user.Email, user.IsActivated, user.ActivatedAt?.DateTime, user.IsDeleted, user.DeletedAt?.DateTime,
            claims, user.Role.Name.ToString());
    }

    public static UserDto GetUserDto(User user, IEnumerable<ClaimDto> claims, string role)
    {
        return new UserDto(user.Id, user.Username, user.Email, user.IsActivated, user.ActivatedAt?.DateTime, user.IsDeleted, user.DeletedAt?.DateTime,
            claims, role);
    }
}