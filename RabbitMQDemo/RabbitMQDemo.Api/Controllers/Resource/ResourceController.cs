using MediatR;
using Microsoft.AspNetCore.Mvc;
using RabbitMQDemo.Api.Controllers.RabbitMQ.Dtos;
using RabbitMQDemo.Api.Controllers.Resource.Dtos;
using RabbitMQDemo.Application.Commands.Resource.CreateBind;
using RabbitMQDemo.Application.Commands.Resource.CreateExchange;
using RabbitMQDemo.Application.Commands.Resource.CreateQueue;
using RabbitMQDemo.Application.Queries.GetExchanges;
using RabbitMQDemo.Application.Queries.GetQueues;
using RabbitMQDemo.Domain.Response;

namespace RabbitMQDemo.Api.Controllers.Resource;

[ApiController]
[Route("Api/[controller]/[action]")]
public class ResourceController : ControllerBase
{
    private readonly ISender _sender;

    public ResourceController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<Result> CreateQueues(List<CreateQueueDto> dto)
    {
        var queues = dto.Select(x => new CreateQueueSubCommand(x.Name, x.Durable, x.Exclusive, x.AutoDelete, x.GetConvertedArguments)).ToList();
        var command = new CreateQueuesCommand(queues);

        var result = await _sender.Send(command);

        return result;
    }

    [HttpGet]
    public async Task<GetQueuesResponse> GetQueues()
    {
        var result = await _sender.Send(new GetQueuesQuery());

        return result.Value;
    }

    [HttpPost]
    public async Task<Result> CreateExchanges(List<CreateExchangeDto> dto)
    {
        var exchanges = dto.Select(x => new CreateExchangeSubCommand(x.Name, x.Type, x.Durable, x.AutoDelete)).ToList();
        var command = new CreateExchangesCommand(exchanges);

        var result = await _sender.Send(command);

        return result;
    }

    [HttpGet]
    public async Task<GetExchangesResponse> GetExchanges()
    {
        var result = await _sender.Send(new GetExchangesQuery());

        return result.Value;
    }

    [HttpPost]
    public async Task<Result> CreateBinds(List<CreateBindDto> dto)
    {
        var binds = dto.Select(x => new CreateBindSubCommand(x.QueueName, x.ExchangeName, x.RoutingKey)).ToList();
        var command = new CreateBindsCommand(binds);

        var result = await _sender.Send(command);

        return result;
    }
}
