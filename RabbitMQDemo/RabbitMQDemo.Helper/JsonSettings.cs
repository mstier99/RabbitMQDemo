using System.Text.Json;

namespace RabbitMQDemo.Helper;

public static class JsonSettings
{
    public static JsonSerializerOptions serializeOptions = new JsonSerializerOptions
    {
        //PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };
}
