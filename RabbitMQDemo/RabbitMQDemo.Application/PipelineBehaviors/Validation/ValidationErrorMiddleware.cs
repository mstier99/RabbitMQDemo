using Microsoft.AspNetCore.Http;
using RabbitMQDemo.Application.PipelineBehaviors.Validation;
using RabbitMQDemo.Helper;
using System.Text.Json;

public sealed class ValidationExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException exception)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            context.Response.ContentType = "application/json";

            var responseJson = JsonSerializer.Serialize(
                new
                {
                    Status = StatusCodes.Status400BadRequest,
                    Detail = exception.Message
                },
                JsonSettings.serializeOptions
            );

            await context.Response.WriteAsync(responseJson);
        }
    }
}