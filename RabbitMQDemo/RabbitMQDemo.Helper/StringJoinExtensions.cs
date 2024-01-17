namespace RabbitMQDemo.Helper;

public static class StringJoinExtensions
{
    public static string ToCommaSepareteted(this IEnumerable<string> collection)
        => string.Join(", ", collection);
}
