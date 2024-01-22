using Authentication.Domain.Entities;
using Authentication.Domain.Entities.Enums;

namespace Authentication.Wrappers;

public record LoginDto(string Username, string Password);

public record TokenDto(string AccessToken, string RefreshToken);

public record RegisterDto(string ActivationCode, string Username, string Password, string ConfirmPassword);

public record NewUserDto(Guid Id, string Username, string Firstname, string Lastname, string Email);

public record NewClaimDto(Guid Guid, string Name, string Value, ClaimType Type);

public record ClaimDto(Guid Id, string Name, string Value)
{
    public static ClaimDto GetClaimDto(Claim claim)
    {
        return new ClaimDto(claim.Id, claim.Name, claim.Value);
    }
}

public record UserDto(
    Guid Id,
    string Username,
    string Email,
    string Firstname,
    string Lastname,
    bool IsActivated,
    DateTime? ActivatedAt,
    bool IsDeleted,
    DateTime? DeletedAt,
    IEnumerable<ClaimDto> Claims,
    string Role)
{
    public static UserDto GetUserDto(User user, IEnumerable<ClaimDto> nodes)
    {
        return new UserDto(user.Id, user.Username, user.Person.Email, user.Person.Firstname, user.Person.Lastname,
            user.IsActivated, user.ActivatedAt, user.IsDeleted, user.DeletedAt,
            nodes, user.Role.Name.ToString());
    }

    public static UserDto CreateDto(User user)
    {
        return new UserDto(user.Id,
            user.Username,
            user.Person.Email,
            user.Person.Firstname,
            user.Person.Lastname,
            user.IsActivated,
            user.ActivatedAt,
            user.IsDeleted,
            user.DeletedAt,
            user.UserClaims.Select(x => ClaimDto.GetClaimDto(x.Claim)),
            user.Role.Name.ToString());
    }
}