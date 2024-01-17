using Microsoft.AspNetCore.Mvc;
using RabbitMQDemo.Application.Commands.User.CreateUser;
using RabbitMQDemo.Application.Commands.Vhost;

namespace RabbitMQDemo.Api.Controllers.Administrator;

[ApiController]
[Route("Api/[controller]/[action]")]
public class AdministratorController : ControllerBase
{
    ISender _sender;

    public AdministratorController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<Result> CreateVhost()
    {
        var result = await _sender.Send(new CreateVhostCommand("test"));
        return result;

    }

    [HttpPost]
    public async Task<Result> CreateUser()
    {
        var result = await _sender.Send(new CreateUserCommand("test", "test", ["administrator"]));
        return result;
    }
}
