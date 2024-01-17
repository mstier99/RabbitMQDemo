using System.Text;

namespace RabbitMQDemo.Domain.Abstraction;

public abstract class Message
{
    public string Vhost { get; private set; }
    public string Exchange { get; private set; }
    public string Body { get; private set; }

    protected Message(string vhost, string exchange, string body)
    {
        Vhost = vhost;
        Exchange = exchange;
        Body = body;
    }

    private byte[] BodyAsByteArrayChace = null!;
    public byte[] BodyAsByteArray => BodyAsByteArrayChace ??= Encoding.UTF8.GetBytes(Body);
}