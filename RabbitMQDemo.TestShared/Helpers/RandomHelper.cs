namespace RabbitMQDemo.TestShared.Helpers;

public static class RandomHelper
{
    public static bool GetTrueOrFalse => new Random().Next(2) == 1;
}
