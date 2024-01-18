using RabbitMQDemo.Api.Controllers.Administrator;
using RabbitMQDemo.Api.Controllers.Resource;

namespace RabbitMQDemo.TestShared.Helpers;

public static class Endpoints
{
    static string Controller = $"Api/{nameof(AdministratorController).Replace("Controller","")}";

    public static string CreateUser => $"{Controller}/{nameof(AdministratorController.CreateUser)}";
    public static string CreateVhost => $"{Controller}/{nameof(AdministratorController.CreateVhost)}";
}
