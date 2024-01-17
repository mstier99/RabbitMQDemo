namespace RabbitMQDemo.Application.Queries.GetQueues;

public record GetQueuesResponse(List<string> QueueNames);
