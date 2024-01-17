namespace RabbitMQDemo.Infra.Repositories.ManagementApi;

public static class ManagementApiEndpoints
{
    // TODO validációs kód nem die való!
    public static List<string> ValidUserTags => new() { "administrator", "monitoring", "policymaker", "impersonator" };

    public static string DeleteUsers => "api/users/bulk-delete";
    public static string GetUsersApi => "api/users";
    public static string GetQueues => "/api/queues";
    public static string GetExchanges => "/api/exchanges";

    /// <summary>
    ///     $"api/users/{userName}"
    /// </summary>
    public static string PutUser => "api/users";

    /// <summary>
    ///     $"api/vhosts/{name}"
    /// </summary>
    public static string PutVhost => "api/vhosts";

    /// <summary>
    ///     $"api/users/{vhost}/{userName}"
    /// </summary>
    public static string PutPermission => "api/permissions";

    /// <summary>
    ///     $"/api/vhosts/{name}"
    /// </summary>
    public static string DeleteVhost => "/api/vhosts";
}