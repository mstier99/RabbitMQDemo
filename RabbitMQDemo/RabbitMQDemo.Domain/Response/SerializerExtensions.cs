using Newtonsoft.Json;

namespace RabbitMQDemo.Domain.Response;

public static class SerializerExtensions
{
    public static Result<T> Deserialize<T>(this string json)
    {
        T? instance = JsonConvert.DeserializeObject<T>(json);

       return instance is null
            ? Error.DeserialozationError<T>(json)
            : instance;
    }
}