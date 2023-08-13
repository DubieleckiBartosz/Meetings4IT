using Newtonsoft.Json;

namespace Meetings4IT.Shared.Implementations.Tools;

public static class BaseSerialize
{
    public static string Serialize<T>(this T data)
    {
        var json = JsonConvert.SerializeObject(data);
        return json;
    }
}