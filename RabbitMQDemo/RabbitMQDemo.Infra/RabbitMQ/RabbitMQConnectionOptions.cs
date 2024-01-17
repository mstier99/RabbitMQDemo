﻿
namespace RabbitMQDemo.Infra.RabbitMQ;

public class RabbitMQConnectionOptions
{
    public required string ClientProvidedName { get; set; }
    public required string HostName { get; set; }
    public required string VirtualHost { get; set; }
    public required int Port { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required ManagamenetApiOptions ManagamenetApiOptions { get; set; }
}

public class ManagamenetApiOptions
{
    public required string Domain { get; set; }
    public required int Port { get; set; }
}
