namespace Notifications.Core.Models.Clients.EmailModels;

public class EmailDetails
{
    public List<string> Recipients { get; set; } 
    public string Subject { get; set; }  
    public string Body { get; set; }  
    public string FromName { get; set; }

    public EmailDetails(List<string> recipients, string subject, string body, string fromName)
    {
        Recipients = recipients;
        Subject = subject;
        Body = body;
        FromName = fromName;
    }
}