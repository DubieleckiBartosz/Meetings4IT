namespace Identities.Core.Responses;
public class ErrorMessages
{
    public static string UserNotFound(string user) => $"User {user} not found.";
}