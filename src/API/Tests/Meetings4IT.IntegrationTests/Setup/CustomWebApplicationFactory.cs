using Meetings4IT.IntegrationTests.Constants;
using Meetings4IT.IntegrationTests.Modules.Mocks;
using Meetings4IT.Shared.Implementations.EventBus.Dispatchers;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventLog.DAL.Repositories;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Panels.Application.Options;
using Panels.Infrastructure.Database;

namespace Meetings4IT.IntegrationTests.Setup;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    //https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-7.0 - xunit.runner.json file is required, otherwise migrations will fail
    /*
     * [ATTENTION]
     * We can use sql server test container https://hamidmosalla.com/2022/09/10/integration-test-in-asp-net-core-6-using-sqlserver-image-and-testcontainers/
     * But we only use integration tests on local environment, so we can use our custom image
     */

    protected override IHost CreateHost(IHostBuilder builder)
    {
        /*
         * When we use the Get method of the IConfiguration interface to bind the values of the appsettings.json file to an object, we need to override this method
         * https://github.com/dotnet/aspnetcore/issues/40681
         */

        builder.ConfigureHostConfiguration(config =>
        {
            config.AddInMemoryCollection(TestSettings.Apply());
        });

        return base.CreateHost(builder);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(TestSettings.TestEnvironment);

        builder.ConfigureTestServices(services =>
        {
            services.Configure<PanelPathOptions>(opts =>
            {
                opts.MeetingPath = "meetingPathTest";
                opts.MeetingDetailsPath = "meetingDetailsPathTest";
                opts.InvitationPath = "invitationPathTest";
                opts.ClientAddress = "https://test";
            });
            services.AddScoped<IAsyncEventDispatcher, TestAsyncEventDispatcher>();
            services.AddScoped<IIntegrationEventLogRepository, TestIntegrationEventLogRepository>();
        });

        builder.ConfigureServices(services =>
        {
            RegisterDatabase<PanelContext>(services);
            services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
            services.AddMvc(_ => _.Filters.Add(new FakeUserFilter()));
        });
    }

    private void RegisterDatabase<T>(IServiceCollection services) where T : DbContext
    {
        var sp = services.BuildServiceProvider();

        using (var scope = sp.CreateScope())
        using (var appContext = scope.ServiceProvider.GetRequiredService<T>())
        {
            try
            {
                appContext.Database.EnsureDeleted();
                appContext.Database.Migrate();
            }
            catch (Exception ex)
            {
                //TEST
                Console.WriteLine(ex?.Message);
            }
        }
    }

    public override ValueTask DisposeAsync()
    {
        using (var scope = Services.CreateScope())
        using (var appContext = scope.ServiceProvider.GetRequiredService<PanelContext>())
        {
            try
            {
                appContext.Database.EnsureDeleted();
            }
            catch (Exception ex)
            {
                //TEST
                Console.WriteLine(ex?.Message);
            }
        }
        return base.DisposeAsync();
    }
}