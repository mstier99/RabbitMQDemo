using RabbitMQDemo.Application.Commands.User.CreatePermission;
using RabbitMQDemo.Application.Commands.User.CreateUser;
using RabbitMQDemo.Application.Commands.User.DeleteUser;
using RabbitMQDemo.Application.Commands.Vhost;
using RabbitMQDemo.Application.Queries.GetExchanges;
using RabbitMQDemo.Application.Queries.GetQueues;
using RabbitMQDemo.Application.Queries.GetUsers;
using RabbitMQDemo.Application.Queries.UsersAreExists;
using RabbitMQDemo.Application.Repositories;
using RabbitMQDemo.Helper;
using RabbitMQDemo.Infra.RabbitMQ;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace RabbitMQDemo.Infra.Repositories.ManagementApi;

public class ManagementApiRepository : IManagementApiRepository
{
    private HttpClient Client => CreateClient();
    private string _defaultVirtualHost = "%2f";
    private readonly string _jsonMediaType = "application/json";
    private readonly RabbitMQConnectionOptions _options;

    public ManagementApiRepository(IOptions<RabbitMQConnectionOptions> options)
    {
        _options = options.Value;
    }

    private HttpClient CreateClient()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri($"{_options.ManagamenetApiOptions.Domain}:{_options.ManagamenetApiOptions.Port}");

        var credentails = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_options.UserName}:{_options.Password}"));

        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentails);

        return client;
    }

    // Put
    public async Task<Result> PutUserAsync(CreateUserCommand command)
    {
        if (command.Tags.All(tag => !ManagementApiEndpoints.ValidUserTags.Contains(tag)))
        {
            throw new Exception($"Paraméterben kapott tag-ek egyike nem érvényes. Tag-ek: {string.Join(",", command.Tags)}. Érvényes tag-ek: {string.Join(",", ManagementApiEndpoints.ValidUserTags)}.");
        }

        var userData = JsonSerializer.Serialize
        (
            new
            {
                password = command.Password,
                tags = string.Join(",", command.Tags)
            }
        );
        var content = new StringContent(userData, Encoding.UTF8, _jsonMediaType);
        var response = await Client.PutAsync($"{ManagementApiEndpoints.PutUser}/{command.UserName}", content);

        return response.IsSuccessStatusCode
            ? Result.Success()
            : Result.Failure(response.GetContentString());
    }

    public async Task<Result> PutPermissionAsync(CreatePermissionCommand command)
    {
        var data = JsonSerializer.Serialize
        (
            new
            {
                configure = command.Configure,
                write = command.Write,
                read = command.Read
            }
        );
        var permissionContent = new StringContent(data, Encoding.UTF8, _jsonMediaType);
        var response = await Client.PutAsync($"{ManagementApiEndpoints.PutPermission}/{command.Vhost}/{command.UserName}", permissionContent);

        return response.IsSuccessStatusCode
            ? Result.Success()
            : Result.Failure(response.GetContentString());
    }

    public async Task<Result> PutVhostAsync(CreateVhostCommand command)
    {
        var response = await Client.PutAsync($"{ManagementApiEndpoints.PutVhost}/{command.Name}", new StringContent("", Encoding.UTF8, _jsonMediaType));

        return response.IsSuccessStatusCode
            ? Result.Success()
            : Result.Failure(response.GetContentString());
    }

    // Delete
    public async Task<Result> DeleteUsersAsync(DeleteUsersCommand command)
    {
        var data = JsonSerializer.Serialize
        (
            new
            {
                users = command.Users
            }
        );
        var content = new StringContent(data, Encoding.UTF8, _jsonMediaType);
        var response = await Client.PostAsync(ManagementApiEndpoints.DeleteUsers, content);

        return response.IsSuccessStatusCode
             ? Result.Success()
             : Result.Failure(response.GetContentString());
    }

    // TODO mediatorral csináld! Pl a command validációra így nem kerül sor
    public async Task<Result> DeleteGuestUserAsync()
        => await DeleteUsersAsync(new DeleteUsersCommand(["guest"]));

    public async Task<Result> DeleteDefaultVhostAsync()
    {
        var response = await Client.DeleteAsync($"{ManagementApiEndpoints.DeleteVhost}/{_defaultVirtualHost}");

        return response.IsSuccessStatusCode
            ? Result.Success()
            : Result.Failure(response.GetContentString());
    }

    // Get
    public async Task<Result<GetUsersResponse>> GetUsersAsync()
    {
        string json = await Client.GetStringAsync(ManagementApiEndpoints.GetUsersApi);

        var deserialize = json.Deserialize<List<GetUsersDeserializerClass>>();

        if (deserialize.IsFailure)
        {
            return deserialize.Error;
        }

        var users = deserialize.Value
           .Select(user => (Name: user.name, Tags: user.tags))
           .ToList();

        return new GetUsersResponse(users);
    }

    public async Task<Result<bool>> UsersAreExistsAsync(UsersAreExistsQuery query)
    {
        // TODO mediator
        var getUsers = await GetUsersAsync();

        if (getUsers.IsFailure)
        {
            return getUsers.Error;
        }

        var users = getUsers.Value.Users;
        var existingUserNames = users.Select(existingUser => existingUser.Name).ToList();

        var usersAreExists = existingUserNames.All(name => existingUserNames.Contains(name));

        return usersAreExists;
    }

    public async Task<Result<GetQueuesResponse>> GetQueuesAsync()
    {
        var json = await Client.GetStringAsync(ManagementApiEndpoints.GetQueues);

        var deserialize = json.Deserialize<List<string>>();

        if (deserialize.IsFailure)
        {
            return deserialize.Error;
        }

        return new GetQueuesResponse(deserialize.Value);
    }

    public async Task<Result<GetExchangesResponse>> GetExchangesAsync()
    {
        var json = await Client.GetStringAsync(ManagementApiEndpoints.GetExchanges);

        var deserialize = json.Deserialize<List<string>>();

        if (deserialize.IsFailure)
        {
            return deserialize.Error;
        }

        return new GetExchangesResponse(deserialize.Value);
    }


    /// <summary>
    ///     Ne nevezd át a tulajdonságokat kicsi betűről nagyra! Nem fog működni a deszerializáció.
    /// </summary>
    class GetUsersDeserializerClass
    {
        public required string name { get; set; }
        public required string[] tags { get; set; }
    }
}