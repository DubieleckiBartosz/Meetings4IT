using Notifications.Core.Models.Clients.EmailModels;

namespace Notifications.Core.Interfaces.Clients;

public interface IEmailClient
{
    Task SendEmailAsync(EmailDetails email);
}