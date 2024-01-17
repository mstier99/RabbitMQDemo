using FluentAssertions;
using NetArchTest.Rules;
using RabbitMQDemo.Application.CQRS;
using RabbitMQDemo.ArchitectureTests.Setup;

namespace RabbitMQDemo.ArchitectureTests.Application;

public class ApplicationTests : Base
{
    [Fact]
    public void CommandHandler_Should_HaveNameEndingWith_CommandHandler()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(ICommandHandler<>))
            .Or()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .Should().HaveNameEndingWith("CommandHandler")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void QueryHandler_Should_HaveNameEndingWith_QueryHandler()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should()
            .HaveNameEndingWith("QueryHandler")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    //[Fact]
    //public void Validator_Should_HaveNameEndingWith_Validator()
    //{
    //    var result = Types.InAssembly(ApplicationAssembly)
    //        .That()
    //        .Inherit(typeof(AbstractValidator<>))
    //        .Should()
    //        .HaveNameEndingWith("Validator")
    //        .GetResult();

    //    result.IsSuccessful.Should().BeTrue();
    //}
}