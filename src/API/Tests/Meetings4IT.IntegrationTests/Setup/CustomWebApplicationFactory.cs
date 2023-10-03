using Identities.Core.DAL;
using Meetings4IT.IntegrationTests.Constants;
using Meetings4IT.IntegrationTests.Modules.Mocks;
using Meetings4IT.Shared.Implementations.EventBus.Dispatchers;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Notifications.Core.Infrastructure.Database;
using Panels.Infrastructure.Database;

namespace Meetings4IT.IntegrationTests.Setup;

public class CustomWebApplicationFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
{
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
            services.AddScoped<IAsyncEventDispatcher, TestAsyncEventDispatcher>();
        });

        builder.ConfigureServices(services =>
        {
            //Configuration options https://blog.markvincze.com/overriding-configuration-in-asp-net-core-integration-tests/
            RegisterLiteContext<PanelContext>(services);
            RegisterLiteContext<NotificationContext>(services);
            RegisterLiteContext<ApplicationDbContext>(services);

            services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
            services.AddMvc(_ => _.Filters.Add(new FakeUserFilter()));
        });
    }

    private void RegisterLiteContext<T>(IServiceCollection services) where T : DbContext
    {
        var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<T>));

        services.Remove(dbContextDescriptor!);
        services.AddDbContext<T>(options =>
        {
            options.UseInMemoryDatabase(TestSettings.ConnectionString);
        });

        var sp = services.BuildServiceProvider();

        using (var scope = sp.CreateScope())
        using (var appContext = scope.ServiceProvider.GetRequiredService<T>())
        {
            try
            {
                appContext.Database.EnsureDeleted();
                appContext.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                //TEST
                Console.WriteLine(ex?.Message);
            }
        }
    }
}