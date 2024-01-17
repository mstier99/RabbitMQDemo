namespace RabbitMQDemo.Api.Controllers.RabbitMQ.Dtos;

public record CreateQueueDto
{
    public required string Name { get; set; }
    public required bool Durable { get; set; }
    public required bool Exclusive { get; set; }
    public required bool AutoDelete { get; set; }

    public List<Argument>? Arguments { get; set; }

    // chache
    private Dictionary<string, object>? _argumentsChace = null;
    public Dictionary<string, object>? GetConvertedArguments
    {
        get
        {
            if (Arguments is null)
            {
                return null;
            }

            return _argumentsChace ??= Arguments.ToDictionary(argument => argument.Name, argument => argument.ConvertedValue);
        }
    }
}


public record Argument(
    string Name, 
    string Value, 
    string Type)
{
    public object ConvertedValue
    {
        get
        {
            return Type.ToLower() switch
            {
                "string" => Value,
                "number" => Convert.ToInt64(Value),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    public static Argument CreateExpireArgument(string value) => new(Names.Expires, value, Types.Number);
    public static Argument CreateDeadletterExchange(string value) => new(Names.DeadletterExchange, value, Types.String);

    private static class Types
    {
        public static string Number => "number";
        public static string String => "string";
    }

    private static class Names
    {
        public static string DeadletterExchange => "x-dead-letter-exchange";
        public static string Expires => "x-expires";
    }
}