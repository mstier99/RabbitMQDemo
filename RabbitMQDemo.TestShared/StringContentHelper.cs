using System.Text;
using System.Text.Json;

namespace RabbitMQDemo.TestShared;

public static class StringContentHelper
{
    static JsonSerializerOptions serializeOptions = new JsonSerializerOptions
    {
        //PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    public static StringContent Create<T>(T @object)
    {
        var json = JsonSerializer.Serialize(@object, serializeOptions);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
}
