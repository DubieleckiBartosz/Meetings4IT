using Notifications.Core.Models.Clients.EmailModels;

namespace Notifications.Core.Interfaces.Clients;

public interface ILocalEmailClient
{
    Task SendAsync(EmailDetails email, EmailOptions emailOptions);
}