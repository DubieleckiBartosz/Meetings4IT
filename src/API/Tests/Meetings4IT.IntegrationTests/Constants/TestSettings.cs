namespace Meetings4IT.IntegrationTests.Constants;

public class TestSettings
{
    public const string TestEnvironment = "TestEnv";

    /*
     * Integration testing depends on our scope. If we want to test the application -> Db, we have to do it as below,
     * otherwise we may mock the repositories, but it's not the best practice
     */
    public const string ConnectionString = "Server=localhost,1440;Database=Meetings4ITTests;User id=sa;Password=sql123456(!)";

    public static Dictionary<string, string> Apply() => new Dictionary<string, string>()
    {
        //Email
        { "EmailOptions:LocalMail", "true" },

        //Quartz
        { "QuartzOptions:Enabled", "false" },

        //Migrations
        { "UsePanelsMigration", "true" },
        { "UseNotificationsMigration", "false" },
        { "UseIdentityMigration", "false" },

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