namespace RabbitMQDemo.TestShared;

public static class Endpoints
{
    // Controllers
    static string ResourceController = "Api/RabbitMQResource";

    // Endpoints
    public static string CreateQueues => $"{ResourceController}/CreateQueues";
    public static string CreateExchanges => $"{ResourceController}/CreateExchanges";
    public static string CreateBinds => $"{ResourceController}/CreateBinds";
}
