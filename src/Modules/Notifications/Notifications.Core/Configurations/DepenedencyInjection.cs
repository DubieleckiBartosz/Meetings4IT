using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Core.Infrastructure.Communication.EmailCommunication;
using Notifications.Core.Infrastructure.Repositories;
using Notifications.Core.Interfaces.Clients;
using Notifications.Core.Interfaces.Repositories;
using Notifications.Core.Models.Clients.EmailModels;

namespace Notifications.Core.Configurations;

public static class DepenedencyInjection
{
    public static WebApplicationBuilder RegisterDepenedencyInjectionNotifications(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var services = builder.Services;

        var options = configuration.GetSection("EmailOptions").Get<EmailOptions>();
        if (options!.LocalMail)
        {
            services.AddScoped<IEmailClient, LocalEmailClient>();
        }

        //Repositories
        services.AddScoped<ITemplateRepository, TemplateRepository>();
        services.AddScoped<IAlertRepository, AlertRepository>();

        return builder;
    }
}