using MediatR;
using Microsoft.AspNetCore.Mvc;
using RabbitMQDemo.Api.Controllers.Message.Dtos;
using RabbitMQDemo.Application.Messages.Fanout;
using RabbitMQDemo.Domain.Response;

namespace RabbitMQDemo.Api.Controllers.Message;

[ApiController]
[Route("Api/[controller]/[action]")]
public class MessageController : ControllerBase
{
    private readonly ISender _sender;

    public MessageController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<Result> SendFanout(FanoutDto dto)
    {
        var result = await _sender.Send(new FanoutCommand("", dto.Exchange, dto.Body));

        return result;
    }
}
