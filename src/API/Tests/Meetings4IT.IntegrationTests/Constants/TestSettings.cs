namespace Meetings4IT.IntegrationTests.Constants;

public class TestSettings
{
    public const string TestEnvironment = "TestEnv";
    public const string ConnectionString = "Meetings4ITMemory";

    public static Dictionary<string, string> Apply() => new Dictionary<string, string>()
    {
        //Email
        { "EmailOptions:LocalMail", "true" },

        //Logging
        { "LoggingOptions:Address", "http://some_address" },

        //OpenIddict
        { "OpenIdDictOptions:ClientId", "TestClientId" },
        { "OpenIdDictOptions:ClientSecret", "TestSecret" },
        { "OpenIdDictOptions:Scope", "TestScope" },

        //EF
        { "EfOptions:ConnectionString", ConnectionString },

        //Dapper
        { "DapperOptions:DefaultConnection", ConnectionString },

        //Identity init
        { "DataInitializationOptions:InsertUserData", "false" },
        { "DataInitializationOptions:InsertRoles", "false" },
        { "DataInitializationOptions:InsertOpenIdDictApplicationConfigurations", "false" },

        //Outbox
        { "OutboxPanelOptions:Enabled", "false" },
    };
}