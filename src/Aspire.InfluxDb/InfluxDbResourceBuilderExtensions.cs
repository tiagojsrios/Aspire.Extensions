using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;

namespace Aspire.InfluxDb;

public static class InfluxDbResourceBuilderExtensions
{
    /// <summary>
    /// Sets the setup mode. According to the documentation, the <paramref name="mode"/> can either be "setup" or "upgrade".
    /// </summary>
    /// <remarks>https://hub.docker.com/_/influxdb</remarks>
    public static IResourceBuilder<InfluxDbResource> WithSetupMode(this IResourceBuilder<InfluxDbResource> builder, string mode)
    {
        return builder.WithEnvironment("DOCKER_INFLUXDB_INIT_MODE", mode);
    }
    
    /// <summary>
    /// Sets the username for the initial admin user.
    /// </summary>
    public static IResourceBuilder<InfluxDbResource> WithUsername(this IResourceBuilder<InfluxDbResource> builder, string username)
    {
        return builder.WithEnvironment("DOCKER_INFLUXDB_INIT_USERNAME", username);
    }
    
    /// <summary>
    /// Sets the password for the initial admin user.
    /// </summary>
    public static IResourceBuilder<InfluxDbResource> WithPassword(this IResourceBuilder<InfluxDbResource> builder, string password)
    {
        return builder.WithEnvironment("DOCKER_INFLUXDB_INIT_PASSWORD", password);
    }
    
    /// <summary>
    /// Sets the name of the initial organization.
    /// </summary>
    public static IResourceBuilder<InfluxDbResource> WithOrganization(this IResourceBuilder<InfluxDbResource> builder, string organization)
    {
        return builder.WithEnvironment("DOCKER_INFLUXDB_INIT_ORG", organization);
    }
    
    /// <summary>
    /// Sets the name of the initial bucket.
    /// </summary>
    public static IResourceBuilder<InfluxDbResource> WithBucket(this IResourceBuilder<InfluxDbResource> builder, string bucket)
    {
        return builder.WithEnvironment("DOCKER_INFLUXDB_INIT_BUCKET", bucket);
    }
    
    /// <summary>
    /// Sets the token for the initial admin user.
    /// </summary>
    public static IResourceBuilder<InfluxDbResource> WithToken(this IResourceBuilder<InfluxDbResource> builder, string token)
    {
        return builder.WithEnvironment("DOCKER_INFLUXDB_INIT_ADMIN_TOKEN", token);
    }
    
    /// <summary>
    /// Sets the retention period for the initial bucket in number of days. When not specified, the default retention period is 0, which means that the data will not expire.
    /// </summary>
    public static IResourceBuilder<InfluxDbResource> WithRetentionPeriod(this IResourceBuilder<InfluxDbResource> builder, int retentionPeriod)
    {
        return builder.WithEnvironment("DOCKER_INFLUXDB_INIT_RETENTION", retentionPeriod.ToString());
    }
}