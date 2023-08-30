using Newtonsoft.Json;

namespace Meetings4IT.Shared.Implementations.Tools;

public static class BaseSerialize
{
    public static string Serialize<T>(this T data, JsonSerializerSettings? settings = null)
    {
        var json = JsonConvert.SerializeObject(data, settings);
        return json;
    }
    
    public static T Deserialize<T>(this string json, Type type,  JsonSerializerSettings? settings = null)
    {
        var result = JsonConvert.DeserializeObject(json, type, settings);
        return (T)result!;
    }
}