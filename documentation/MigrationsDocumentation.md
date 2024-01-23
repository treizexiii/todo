
## Identity:

```bash
dotnet ef migrations add 'Init' --project .\src\Authentication\Authentication.Persistence\Authentication.Persistence.csproj --startup-project .\src\Authentication\Authentication.IdentityServer\Authentication.IdentityServer.csproj --context IdentityDb
```

```bash
dotnet ef database update --project .\src\Authentication\Authentication.Persistence\Authentication.Persistence.csproj --startup-project .\src\Authentication\Authentication.IdentityServer\Authentication.IdentityServer.csproj --context IdentityDb
```

## Todo:

```bash
dotnet ef migrations add 'init' --project .\src\Database\Database.csproj --startup-project .\src\Api\Api.csproj --context TodoDb
```

```bash
dotnet ef database update --project .\src\Database\Database.csproj --startup-project .\src\Api\Api.csproj --context TodoDb
```