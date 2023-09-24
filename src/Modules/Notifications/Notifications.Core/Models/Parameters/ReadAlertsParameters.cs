using Newtonsoft.Json;

namespace Notifications.Core.Models.Parameters;

public class ReadAlertsParameters
{
    public List<int> AlertIdentifiers { get; init; }

    public ReadAlertsParameters()
    { }

    [JsonConstructor]
    public ReadAlertsParameters(List<int> alertIdentifiers)
    {
        AlertIdentifiers = alertIdentifiers;
    }
}