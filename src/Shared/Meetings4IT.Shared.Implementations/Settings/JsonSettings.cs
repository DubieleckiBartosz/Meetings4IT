using Meetings4IT.Shared.Implementations.Tools;
using Newtonsoft.Json;

namespace Meetings4IT.Shared.Implementations.Settings;

public class JsonSettings
{
    public static JsonSerializerSettings DefaultSerializerSettings => new () 
    {
        ContractResolver = new PrivateResolver(),
        ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
        TypeNameHandling = TypeNameHandling.All 
    };
}