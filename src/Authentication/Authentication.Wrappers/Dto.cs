using Authentication.Domain.Entities;
using Authentication.Domain.Entities.Enums;

namespace Authentication.Wrappers;

public record LoginDto(string Username, string Password);

public record RegisterDto(string Username, string Password);

public record NewUserDto(Guid Id, string Username, string Email);

public record NewClaimDto(Guid Guid, string Name, string Value, ClaimType Type);

public record ClaimDto(Guid Id, string Name, string Value)
{
    public static ClaimDto GetClaimDto(Claim claim)
    {
        return new ClaimDto(claim.Id, claim.Name, claim.Value);
    }
}

public record UserDto(Guid Id, string Username, string Email, bool IsActivated, DateTime? ActivatedAt, bool IsDeleted,
    DateTime? DeletedAt, IEnumerable<ClaimDto> Nodes, string Roles)
{
    public static UserDto GetUserDto(User user, IEnumerable<ClaimDto> nodes)
    {
        return new UserDto(user.Id, user.Username, user.Email, user.IsActivated, user.ActivatedAt, user.IsDeleted, user.DeletedAt,
            nodes, user.Role.Name.ToString());
    }

    public static UserDto GetUserDto(User user, IEnumerable<ClaimDto> nodes, string roles)
    {
        return new UserDto(user.Id, user.Username, user.Email, user.IsActivated, user.ActivatedAt, user.IsDeleted, user.DeletedAt,
            nodes, roles);
    }
}