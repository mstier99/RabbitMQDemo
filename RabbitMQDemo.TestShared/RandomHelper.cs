namespace RabbitMQDemo.TestShared;

public static class RandomHelper
{
    public static bool GetTrueOrFalse => new Random().Next(2) == 1;
}
