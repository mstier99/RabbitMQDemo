using Microsoft.AspNetCore.Mvc;
using RabbitMQDemo.Api.Controllers.Administrator.Dtos;
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
    public async Task<Result> CreateVhost(CreateVhostDto dto) 
        => await _sender.Send(new CreateVhostCommand(dto.Name));

    [HttpPost]
    public async Task<Result> CreateUser(CreateUserDto dto) 
        => await _sender.Send(new CreateUserCommand(dto.UserName, dto.Password, dto.Tags));
}
