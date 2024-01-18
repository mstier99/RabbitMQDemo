using RabbitMQDemo.Api.Controllers.Resource;

namespace RabbitMQDemo.TestShared.Helpers;

public static class Endpoints
{
    static string Controller = $"Api/{nameof(ResourceController).Replace("Controller","")}";

    public static string CreateQueues => $"{Controller}/{nameof(ResourceController.CreateQueues)}";
    public static string CreateExchanges => $"{Controller}/{nameof(ResourceController.CreateExchanges)}";
    public static string CreateBinds => $"{Controller}/{nameof(ResourceController.CreateBinds)}";
}
