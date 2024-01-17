namespace RabbitMQDemo.Domain.Response;

public record Error(string Code, string Details)
{
    public bool IsNone() => this == None;

    // Instances
    public static Error None => new("", "");

    public static Error NullValue => new("Error.NullValue", "Null value was provided.");

    public static Error DeserialozationError() => new("Error.Deserialization", $"Deserialization faild!");

    public static Error DeserialozationError<T>(string json) => new("Error.Deserialization", $"Deserialization faild! Json: {json}, target type:{typeof(T).FullName}.");

    public static Error WithDynamicDetails(string details) => new("Error.Dynamic", details);
}
