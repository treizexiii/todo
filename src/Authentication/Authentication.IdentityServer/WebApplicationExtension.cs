using Authentication.Persistence;
using Authentication.Persistence.Database;

namespace Authentication.IdentityServer;

public static class WebApplicationExtension
{
    public static WebApplication ControlDatabase(this WebApplication app)
    {
        var context = app.Services.GetRequiredService<IdentityDb>();
        var migrator = new DataBaseMigrator(context);
        migrator.CheckDatabase().Wait();
        return app;
    }
}