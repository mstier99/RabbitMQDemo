using MediatR;
using Microsoft.Extensions.Logging;
using RabbitMQDemo.Domain.Response;
using System.Diagnostics;
using System.Reflection;

namespace RabbitMQDemo.Application.PipelineBehaviors;

public class LoggingPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;

    public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var reuqestType = typeof(TRequest);

        _logger.LogInformation(
            "Start Handle {@RequestName}",
            reuqestType);

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var result = await next();

        if (result.IsFailure)
        {
            _logger.LogError(
                "Handle Complited with error {@RequestName} in {@Elapsed} ms, {@Error}",
                reuqestType.Name,
                stopwatch.Elapsed.TotalMilliseconds,
                result.Error);
        }
        else
        {
            object? value = GetValue(result);

            _logger.LogInformation(
                "Handle Complited {@RequestName} in {@Elapsed} ms, {@Value}",
                reuqestType.Name,
                stopwatch.Elapsed.TotalMilliseconds,
                value);
        }

        return result;
    }

    private object? GetValue(TResponse result)
    {
        // Result<T> típusnak az eredménye a "Value" tulajdonságban van.
        PropertyInfo? valuePropertyInfo = result?.GetType().GetProperty("Value");
        object? value = valuePropertyInfo?.GetValue(result, index: null);

        return value;
    }
}
