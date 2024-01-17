using FluentValidation;

namespace RabbitMQDemo.Application.Commands.Resource.CreateQueue;

public class CreateQueuesCommandValidator : AbstractValidator<CreateQueuesCommand>
{
    public CreateQueuesCommandValidator()
    {
        RuleFor(x => x).Must(x => true);
    }

    //private bool AutoDeleteAndDurable(CreateQueuesCommand command)
    //{
    //	foreach (var queue in command.Queues)
    //	{
    //		if(queue.Durable && queue.AutoDelete)
    //		{

    //		}
    //	}
    //}
}
