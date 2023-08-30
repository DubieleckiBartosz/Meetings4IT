using Meetings4IT.Shared.Implementations.Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using Meetings4IT.Shared.Abstractions.Events;
using Meetings4IT.Shared.Implementations.Decorators;
using Meetings4IT.Shared.Implementations.Behaviours;
using Meetings4IT.Shared.Implementations.Dapper;
using Meetings4IT.Shared.Implementations.EventBus;
using Meetings4IT.Shared.Implementations.EventBus.Dispatchers;
using Meetings4IT.Shared.Implementations.EventBus.InMemoryMessaging;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventLog.DAL.Repositories;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventLog.Services;
using Meetings4IT.Shared.Implementations.Options;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventLog.DAL;
using Serilog.Events;
using Serilog;
using Meetings4IT.Shared.Implementations.Reference;
using Microsoft.Extensions.Configuration;
using Serilog.Exceptions;
using Meetings4IT.Shared.Implementations.Services;
using Microsoft.EntityFrameworkCore;
using Meetings4IT.Shared.Implementations.EventBus.Channel;

namespace Meetings4IT.Shared.Implementations;

public static class Configurations
{
    public static WebApplicationBuilder RegistrationSharedConfigurations(this WebApplicationBuilder builder,
        List<Type> integrationEventTypes, bool withDapper = true, params Type[] assemblyTypes)
    {
        //MEDIATOR
        var services = builder.Services;
        var config = builder.Configuration;

        services.AddTransient<IDomainDecorator, MediatorDecorator>();
        services.RegisterMediator(assemblyTypes);

        if (withDapper)
        {
            services.Configure<DapperOptions>(config.GetSection(nameof(DapperOptions)));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
            services.AddScoped<ITransactionSupervisor, TransactionSupervisor>();
        }

        //EVENT BUS (in memory)
        services.AddScoped<IEventBus, InMemoryEventBus>();
        services.AddSingleton<IEventRegistry, EventRegistry>();

        //Channel
        services.AddSingleton<IEventChannel, EventChannel>();

        //Dispatchers
        services.AddSingleton<IAsyncEventDispatcher, AsyncEventDispatcher>(); 
        services.AddSingleton<IEventDispatcher, EventDispatcher>();

        //Worker
        services.AddHostedService<AsyncDispatcherJob>();

        //PIPELINES
        services.RegisterValidatorPipeline();

        //INTEGRATION LOG REPO
        services.AddScoped<IntegrationEventLogContext>();
        services.AddTransient<IIntegrationEventLogRepository, IntegrationEventLogRepository>();
        services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService>();

        return builder;
    }

    public static IServiceCollection RegisterAutoMapper(this IServiceCollection services, params Type[] types)
    {
        var assemblies = types.Select(_ => _.GetTypeInfo().Assembly);

        foreach (var assembly in assemblies)
        {
            services.AddAutoMapper(assembly);
        }

        return services;
    }
     
    public static WebApplicationBuilder RegisterUserAccessor(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddTransient<ICurrentUser, CurrentUser>();

        return builder;
    }

    public static IServiceCollection RegisterMediator(this IServiceCollection services, params Type[] types)
    {
        //MEDIATOR
        var assemblies = types.Select(_ => _.GetTypeInfo().Assembly);

        foreach (var assembly in assemblies)
        {
            services.AddMediatR(assembly);
        }

        services.AddMediatR(typeof(SharedAssemblyReference).GetTypeInfo().Assembly);

        services.AddTransient<ICommandBus, CommandBus>();
        services.AddTransient<IQueryBus, QueryBus>();

        return services;
    }

    public static IServiceCollection RegisterValidatorPipeline(this IServiceCollection services)
    {
        //VALIDATOR PIPELINE
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));

        return services;
    }

    public static void LogConfigurationService(this LoggerConfiguration loggerConfiguration, IConfiguration configuration)
    { 
        var logging = new LoggingOptions();
        configuration.GetSection(nameof(LoggingOptions)).Bind(logging);

        var dateTimeNowString = $"{DateTime.Now:yyyy-MM-dd}";

        loggerConfiguration
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.WithExceptionDetails()
            .Enrich.FromLogContext()
            .WriteTo.Logger(
                _ => _.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error)
                    .WriteTo.File($"Logs/{dateTimeNowString}-Error.log",
                        rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 100000)
            )
            .WriteTo.Logger(
                _ => _.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning)
                    .WriteTo.File($"Logs/{dateTimeNowString}-Warning.log",
                        rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 100000)
            )
            .WriteTo.File($"Logs/{dateTimeNowString}-All.log")
            .WriteTo.Console() 
            .WriteTo.Seq(logging.Address!); 
    }

    public static WebApplicationBuilder RegisterEntityFrameworkSqlServer<T>(this WebApplicationBuilder builder,
        Func<DbContextOptionsBuilder, DbContextOptionsBuilder>? additionalRegistrations = null) where T : DbContext
    {
        var options = builder.Configuration.GetSection("EfOptions").Get<EfOptions>()!;
        builder.Services.AddDbContext<T>(dbContextBuilder =>
        {
            dbContextBuilder.UseSqlServer(options.ConnectionString);

            additionalRegistrations?.Invoke(dbContextBuilder);
        });

        return builder;
    }
}