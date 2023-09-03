using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Core.Models.Clients.EmailModels;

namespace Notifications.Core.Configurations;

public static class OptionsConfigurations
{
    public static WebApplicationBuilder RegisterNotificationsOptions(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var services = builder.Services;

        services.Configure<EmailOptions>(configuration.GetSection("EmailOptions"));

        return builder;
    }
}