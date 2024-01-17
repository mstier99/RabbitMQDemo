namespace RabbitMQDemo.Helper;

public static class HttpResponseMessageExtensions
{
    public static string GetContentString(this HttpResponseMessage message) => message.Content.ReadAsStringAsync().Result;
}
