using RabbitMQDemo.Application.Messages.Fanout;
using RabbitMQDemo.Domain.Abstraction;
using RabbitMQDemo.Infra.RabbitMQ;
using System.Reflection;

namespace RabbitMQDemo.ArchitectureTests.Setup;

public class Base
{
    protected static Assembly DomainAssembly => typeof(Message).Assembly;
    protected static Assembly ApplicationAssembly => typeof(FanoutCommand).Assembly;
    protected static Assembly InfraAssembly => typeof(RabbitMQConnection).Assembly;
}
