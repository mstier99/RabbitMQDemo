using RabbitMQDemo.Helper;
using System.Text;
using System.Text.Json;

namespace RabbitMQDemo.TestShared.Helpers;

public static class StringContentHelper
{
    public static StringContent Create<T>(T @object)
    {
        var json = JsonSerializer.Serialize(@object, JsonSettings.serializeOptions);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
}
