using Notifications.Core.Interfaces.Clients;
using Notifications.Core.Models.Clients.EmailModels;
using System.Net.Mail;

namespace Notifications.Core.Infrastructure.Communication.EmailCommunication;

public class LocalEmailClient : ILocalEmailClient
{
    public async Task SendAsync(EmailDetails email, EmailOptions emailOptions)
    {
        var message = new MailMessage
        {
            From = new MailAddress(emailOptions.FromAddress),
            Subject = email.Subject,
            Body = email.Body,
            IsBodyHtml = true
        };
        foreach (var recipient in email.Recipients)
        {
            message.To.Add(new MailAddress(recipient));
        }

        using var client = new SmtpClient(emailOptions.Host, emailOptions.Port);
        await client.SendMailAsync(message);
    }
}