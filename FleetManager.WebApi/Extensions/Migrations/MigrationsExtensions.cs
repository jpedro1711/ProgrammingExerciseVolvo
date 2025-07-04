using FleetManager.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.WebApi.Extensions.Migrations
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<FleetManagerDatabaseContext>();
            dbContext.Database.Migrate();
        }
    }
}
