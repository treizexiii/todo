using Authentication.Domain.Entities.Enums;

namespace Authentication.Domain.Entities;

public class Role
{
    public Guid Id { get; set; }
    public RoleEnum Name { get; set; } = RoleEnum.User;
}

public static class RoleType
{
    public static List<Role> Roles => new()
    {
        new Role {Id = Guid.NewGuid(), Name = RoleEnum.Admin},
        new Role {Id = Guid.NewGuid(), Name = RoleEnum.User}
    };
}