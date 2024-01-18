using RabbitMQDemo.Helper;

namespace RabbitMQDemo.Application.PipelineBehaviors.Validation;

[Serializable]

public class ValidationException : Exception
{
    public ValidationException() : base() { }
    public ValidationException(string message) : base(message) { }
    public ValidationException(string message, Exception inner) : base(message, inner) { }

    public ValidationException(List<ValidationResult> result, Exception inner) 
        : base(CreateMessage(result), inner) { }

    public ValidationException(List<ValidationResult> result)
       : base(CreateMessage(result)) { }


    private static string CreateMessage(List<ValidationResult> result)
    {
        var message = result.Select(x => $"{x.PropertyName}: {x.ErrorMessage}").ToList();

        return $"Validation error occurred! The following properties are not valid! \r\n {message.ToCommaSepareteted()} \r\n";
    }
}
