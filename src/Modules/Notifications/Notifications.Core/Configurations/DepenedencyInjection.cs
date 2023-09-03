using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Core.Infrastructure.Communication.EmailCommunication;
using Notifications.Core.Interfaces.Clients;
using Notifications.Core.Models.Clients.EmailModels;

namespace Notifications.Core.Configurations;

public static class DepenedencyInjection
{
    public static WebApplicationBuilder RegisterDepenedencyInjectionNotifications(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var options = configuration.GetSection("EmailOptions").Get<EmailOptions>();
        if (options!.LocalMail)
        {
            builder.Services.AddScoped<IEmailClient, LocalEmailClient>();
        }

        return builder;
    }
}