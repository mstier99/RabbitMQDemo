using FluentValidation;
using RabbitMQDemo.Helper;

namespace RabbitMQDemo.Application.Commands.User.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public static List<string> _validUserTags => new() { "administrator", "monitoring", "policymaker", "impersonator" };

    public CreateUserCommandValidator()
    {
        RuleFor(x => x)
            .Must(TagsIsValid)
            .WithMessage(command => $"Paraméterben kapott tag-ek egyike nem érvényes. Tag-ek: {command.Tags.ToCommaSepareteted()}. Érvényes tag-ek: {_validUserTags.ToCommaSepareteted()}.");
    }

    private bool TagsIsValid(CreateUserCommand command)
    {
        if (command.Tags.All(tag => !_validUserTags.Contains(tag)))
        {
            return false;
        }

        return true;
    }
}
