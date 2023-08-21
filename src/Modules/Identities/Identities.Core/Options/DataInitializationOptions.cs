namespace Identities.Core.Options;

public class DataInitializationOptions
{
    public bool InsertUserData { get; set; }
    public bool InsertRoles { get; set; }
    public bool InsertOpenIdDictApplicationConfigurations { get; set; }
}