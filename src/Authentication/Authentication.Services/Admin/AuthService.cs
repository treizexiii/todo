using Authentication.Domain.Entities;
using Authentication.Domain.Repositories;
using Authentication.Wrappers;

namespace Authentication.Services.Admin;

public interface IAUthService
{
    Task<UserDto> RegisterAsync(RegisterDto register);
    Task<UserDto> ControlUserAsync(LoginDto login);
    Task<TokenDto> ProduceToken(User user);
    Task<UserDto> ControlTokenAsync(string? refreshToken);
}

internal class AuthService(IUserRepository usersRepository) : IAUthService
{
    public async Task<UserDto> RegisterAsync(RegisterDto register)
    {
        var user = await usersRepository.GetUserAsync(register.Username);
        if (user == null || user.IsDeleted || user.IsActivated)
        {
            throw new UnauthorizedAccessException("Credentials are not valid");
        }

        user.IsActivated = false;
        user.ActivatedAt = DateTime.UtcNow;

        var dto = UserDto.CreateDto(user);

        return dto;
    }

    public Task<UserDto> ControlUserAsync(LoginDto login)
    {
        throw new NotImplementedException();
    }

    public Task<TokenDto> ProduceToken(User user)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> ControlTokenAsync(string? refreshToken)
    {
        throw new NotImplementedException();
    }
}