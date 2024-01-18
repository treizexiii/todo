using Authentication.Domain.Entities.Enums;

namespace Authentication.Domain.Entities;

public class Role
{
    public Guid Id { get; set; }
    public RoleEnum Name { get; set; } = RoleEnum.User;
}