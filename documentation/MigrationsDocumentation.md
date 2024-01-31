
## Create migrations

### Identity:

```bash
dotnet ef migrations add 'Init' --project .\src\Authentication\Authentication.Persistence\Authentication.Persistence.csproj --startup-project .\src\Authentication\Authentication.IdentityServer\Authentication.IdentityServer.csproj --context IdentityDb
```

```bash
dotnet ef database update --project .\src\Authentication\Authentication.Persistence\Authentication.Persistence.csproj --startup-project .\src\Authentication\Authentication.IdentityServer\Authentication.IdentityServer.csproj --context IdentityDb
```

### Todo:

```bash
dotnet ef migrations add 'init' --project .\src\Persistence\Persistence.Database\Persistence.Database.csproj --startup-project .\src\Persistence\Persistence.MigrationTool\Persistence.MigrationTool.csproj --context TodoDb
```

## Update database

From db-command container, run the following commands:

```bash
dotnet Persistence.MigrationTool.dll create-database # for creating the database
```

```bash
dotnet Persistence.MigrationTool.dll update-database # for updating the database
```

### old:

```bash
dotnet ef database update --project .\src\Database\Database.csproj --startup-project .\src\Api\Api.csproj --context TodoDb
```
