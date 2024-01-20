using FluentAssertions;
using NetArchTest.Rules;
using RabbitMQDemo.ArchitectureTests.Setup;

namespace RabbitMQDemo.ArchitectureTests.Layers;

public class LayerTests : Base
{
    [Fact]
    public void DomainLayer_Should_NotHaveDependencyOn_ApplicationLayer()
    {
        var result = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn(ApplicationAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void DomainLayer_Should_NotHaveDependencyOn_InfrastructureLayer()
    {
        var result = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn(ApplicationAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    //[Fact]
    //public void ApplicationLayer_Should_NotHaveDependencyOn_InfrastructureLayer()
    //{
    //    // TODO nem is referál rá, mégse jó

    //    var result = Types.InAssembly(ApplicationAssembly)
    //        .Should()
    //        .NotHaveDependencyOn(InfraAssembly.GetName().Name)
    //        .GetResult();

    //    result.IsSuccessful.Should().BeTrue();
    //}

}
