using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace RiffRecords.Infrastructure.Persistence
{
    public static class DatabaseMigrationExtensions
    {
        public static async Task MigrateDatabaseAsync(this IServiceProvider services, CancellationToken ct = default)
        {
            using var scope = services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await db.Database.MigrateAsync(ct);
        }
    }
}
