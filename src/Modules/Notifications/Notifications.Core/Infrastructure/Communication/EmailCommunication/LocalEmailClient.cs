using Microsoft.Extensions.Options;
using Notifications.Core.Interfaces.Clients;
using Notifications.Core.Models.Clients.EmailModels;
using System.Net.Mail;

namespace Notifications.Core.Infrastructure.Communication.EmailCommunication;

public class LocalEmailClient : IEmailClient
{
    private readonly EmailOptions _options;

    public LocalEmailClient(IOptions<EmailOptions> options)
    {
        _options = options.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task SendEmailAsync(EmailDetails email)
    {
        var message = new MailMessage
        {
            From = new MailAddress(_options.FromAddress),
            Subject = email.Subject,
            Body = email.Body,
            IsBodyHtml = true,
        };

        foreach (var recipient in email.Recipients)
        {
            message.To.Add(new MailAddress(recipient));
        }

        using var client = new SmtpClient(_options.Host, _options.Port);
        await client.SendMailAsync(message);
    }
}