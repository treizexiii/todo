using System.Text.Json;
using System.Text.Json.Serialization;
using Authentication.Domain.Entities;

namespace Authentication.Persistence.PersistentEntities;

public class DbUser : User
{
    public string PersonData { get; set; } = string.Empty;

    public User ToUser()
    {
        var user = (User)this;
        user.Person = JsonSerializer.Deserialize<Person>(PersonData)
            ?? throw new NullReferenceException("Could not deserialize PersonData");

        return user;
    }

    public static DbUser Create(User user)
    {
        var dbUser = (DbUser)user;
        dbUser.PersonData = JsonSerializer.Serialize(dbUser.Person);

        return dbUser;
    }
}