namespace Panels.Domain.Generators;

public class InvitationCodeGenerator
{
    private const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public static string GenerateCode(int prefix)
    {
        var rnd = new Random();
        var length = 5;
        var guidId = Guid.NewGuid().ToString("N").Substring(0, length).ToUpper();
        var randomLetters = new string(Enumerable.Repeat(characters, length)
            .Select(_ => _[rnd.Next(_.Length)]).ToArray());

        return $"{prefix}{randomLetters}{guidId}";
    }
}