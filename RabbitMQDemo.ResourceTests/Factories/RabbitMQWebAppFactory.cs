using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Text;
using Testcontainers.RabbitMq;
using System.Text.RegularExpressions;
using RabbitMQDemo.Infra.RabbitMQ;

namespace RabbitMQDemo.ResourceTests.Factories;

public class RabbitMQWebAppFactory : WebApplicationFactory<Api.Program>, IAsyncLifetime
{
    const string _hostName = "brokerhost";
    const int _defaultManagementApiPort = 15672;
    const int _defaultPort = 5672;
    string? _userName;
    string? _password;


    RabbitMqContainer _rabbitMqContainer = new RabbitMqBuilder()
        // Image
        .WithImage("rabbitmq:3.12-management")
        // Ports
        .WithPortBinding(_defaultPort, assignRandomHostPort: true)
        .WithPortBinding(_defaultManagementApiPort, assignRandomHostPort: true)
        //Host
        .WithHostname(_hostName)
        // End
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.Configure<RabbitMQConnectionOptions>(option =>
            {
                string pattern = @"amqp://(?<username>[^:]+):(?<password>[^@]+)@(?<host>[^:]+):(?<port>\d+)/";
                Match match = Regex.Match(_rabbitMqContainer.GetConnectionString(), pattern);

                option.ClientProvidedName = $"RabbitMQDemo Test";
                option.HostName = _rabbitMqContainer.Hostname;
                option.Port = Convert.ToInt32(_rabbitMqContainer.GetMappedPublicPort(_defaultPort));
                option.UserName = _userName = match.Groups["username"].Value;
                option.Password = _password = match.Groups["password"].Value;

                option.ManagamenetApiOptions.Port = _rabbitMqContainer.GetMappedPublicPort(_defaultManagementApiPort);
                option.ManagamenetApiOptions.Domain = _hostName;
            });
        });
    }

    /// <summary>
    ///     Ez a kliens a gazda gépről küld http kéréseket ÉS NEM A DOCKER BELSŐ HÁLOZATRÓL!
    /// </summary>
    public HttpClient CreateManagementApiClient()
    {
        string baseAddress = $"http://localhost:{_rabbitMqContainer.GetMappedPublicPort(_defaultManagementApiPort)}";

        var client = new HttpClient();
        client.BaseAddress = new Uri(baseAddress);

        var credentails = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_userName}:{_password}"));

        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentails);

        return client;
    }

    Task IAsyncLifetime.InitializeAsync() => _rabbitMqContainer.StartAsync();
    Task IAsyncLifetime.DisposeAsync() => _rabbitMqContainer.DisposeAsync().AsTask();
}
