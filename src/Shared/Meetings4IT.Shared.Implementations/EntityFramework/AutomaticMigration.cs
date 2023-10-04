using Meetings4IT.Shared.Abstractions.Time;
using Meetings4IT.Shared.Implementations.Tools;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Meetings4IT.Shared.Implementations.EntityFramework;

public static class AutomaticMigration
{
    public static WebApplication RunMigration<TContext>(this WebApplication app) where TContext : DbContext
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider
                .GetRequiredService<TContext>();

            var logger = scope.ServiceProvider
                .GetRequiredService<ILogger>();
            try
            {
                if (dbContext.Database.CanConnect())
                {
                    if (dbContext.Database.IsRelational())
                    {
                        var pendingMigrations = dbContext.Database?.GetPendingMigrations()?.ToList();
                        if (pendingMigrations != null && pendingMigrations.Any())
                        {
                            logger.Information($"Before migrations : {string.Join(", ", pendingMigrations)} - {Clock.CurrentDate()}");
                            dbContext.Database?.Migrate();
                            logger.Information($"After migrations - {Clock.CurrentDate()}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(new
                {
                    Message = ex.Message,
                    MigrationException = ex.InnerException,
                }.Serialize());
                throw;
            }
        }

        return app;
    }
}