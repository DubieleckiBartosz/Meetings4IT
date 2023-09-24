using MediatR;
using Meetings4IT.Shared.Abstractions.Events;
using Meetings4IT.Shared.Implementations.Behaviors;
using Meetings4IT.Shared.Implementations.Dapper;
using Meetings4IT.Shared.Implementations.Decorators;
using Meetings4IT.Shared.Implementations.EventBus;
using Meetings4IT.Shared.Implementations.EventBus.Channel;
using Meetings4IT.Shared.Implementations.EventBus.Dispatchers;
using Meetings4IT.Shared.Implementations.EventBus.InMemoryMessaging;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventLog.DAL;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventLog.DAL.Repositories;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventLog.Services;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;
using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Modules;
using Meetings4IT.Shared.Implementations.Modules.Interfaces;
using Meetings4IT.Shared.Implementations.Options;
using Meetings4IT.Shared.Implementations.Reference;
using Meetings4IT.Shared.Implementations.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using System.Reflection;

namespace Meetings4IT.Shared.Implementations;

public static class Configurations
{
    public static WebApplicationBuilder RegistrationSharedConfigurations(this WebApplicationBuilder builder,
        bool withDapper = true, params Type[] assemblyTypes)
    {
        //MEDIATOR
        var services = builder.Services;
        var config = builder.Configuration;

        services.AddTransient<IDomainDecorator, MediatorDecorator>();

        services.RegisterMediator(assemblyTypes);

        services.Configure<LogOptions>(config.GetSection(nameof(LogOptions)));

        if (withDapper)
        {
            services
                .Configure<DapperOptions>(config.GetSection(nameof(DapperOptions)))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>))
                .AddScoped<ITransactionSupervisor, TransactionSupervisor>();
        }

        //MODULE CLIENT
        services
            .AddSingleton<IModuleClient, ModuleClient>()
            .AddSingleton<IModuleSubscriber, ModuleSubscriber>()
            .AddSingleton<IModuleActionRegistration, ModuleActionRegistration>();

        //EVENT BUS (in memory)
        services
            .AddScoped<IEventBus, InMemoryEventBus>()
            .AddSingleton<IEventRegistry, EventRegistry>();

        //Channel
        services.AddSingleton<IEventChannel, EventChannel>();

        //Dispatchers
        services
            .AddSingleton<IAsyncEventDispatcher, AsyncEventDispatcher>()
            .AddSingleton<IEventDispatcher, EventDispatcher>();

        //Worker
        services.AddHostedService<AsyncDispatcherJob>();

        //PIPELINES
        services.RegisterValidatorPipeline();

        //INTEGRATION LOG REPO
        services
            .AddScoped<IntegrationEventLogContext>()
            .AddTransient<IIntegrationEventLogRepository, IntegrationEventLogRepository>()
            .AddTransient<IIntegrationEventLogService, IntegrationEventLogService>();

        //Integration service
        services.AddScoped<IIntegrationService, IntegrationService>();

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
        builder.Services
            .AddHttpContextAccessor()
            .AddTransient<ICurrentUser, CurrentUser>();

        return builder;
    }

    public static IServiceCollection RegisterMediator(this IServiceCollection services, params Type[] types)
    {
        //MEDIATOR
        services.AddMediatR(types);
        services.AddMediatR(typeof(SharedAssemblyReference));

        services
            .AddTransient<ICommandBus, CommandBus>()
            .AddTransient<IQueryBus, QueryBus>();

        return services;
    }

    public static IServiceCollection RegisterValidatorPipeline(this IServiceCollection services)
    {
        //VALIDATOR PIPELINE
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

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

    public static WebApplicationBuilder RegisterEntityFrameworkSqlServer<T>(this WebApplicationBuilder builder, EfOptions efOptions,
        Func<DbContextOptionsBuilder, DbContextOptionsBuilder>? additionalRegistrations = null, ILoggerFactory? loggerFactory = null) where T : DbContext
    {
        builder.Services.AddDbContext<T>(dbContextBuilder =>
        {
            dbContextBuilder.UseSqlServer(efOptions.ConnectionString);
            if (loggerFactory != null)
            {
                dbContextBuilder.UseLoggerFactory(loggerFactory);
            }

            additionalRegistrations?.Invoke(dbContextBuilder);
        });

        return builder;
    }

    public static IModuleSubscriber UseModuleRequests(this IApplicationBuilder app)
    => app.ApplicationServices.GetRequiredService<IModuleSubscriber>();
}