using Microsoft.EntityFrameworkCore;
using SimpleMediatr.Data.Database;

namespace SimpleMediatR.Extensions
{
    public static class MigrationManager
    {
        public static WebApplication MigrateDatabase(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<SimpleMediatrDataContext>())
                {
                    try
                    {
                        appContext.Database.Migrate();
                    }
                    catch (Exception)
                    {
                        throw new Exception("Migration failed");
                    }
                }
            }
            return app;
        }
    }
}
