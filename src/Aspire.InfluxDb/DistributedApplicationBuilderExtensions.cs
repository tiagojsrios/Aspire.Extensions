using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;

namespace Aspire.InfluxDb;

public static class DistributedApplicationBuilderExtensions
{
    /// <summary>
    /// Configures an InfluxDb resource in the Aspire application host. 
    /// </summary>
    /// <param name="builder">The <see cref="IDistributedApplicationBuilder"/></param>
    /// <param name="name">Name of the resource</param>
    /// <param name="entrypoint">Command that will be run by the resource during startup</param>
    /// <param name="tag">Allows you to override the default image tag that will be retrieved. Defaults to <see cref="InfluxDbContainerImageTags.Tag"/></param>
    /// <param name="port">When specified, the resource will be created with a port mapping between the host and the container.</param>
    public static IResourceBuilder<InfluxDbResource> AddInfluxDb(this IDistributedApplicationBuilder builder, [ResourceName] string name, 
        string? entrypoint = null, string? tag = null, int? port = null)
    {
        InfluxDbResource resource = new(name, entrypoint);

        IResourceBuilder<InfluxDbResource> resourceBuilder = builder.AddResource(resource)
            .WithImage(InfluxDbContainerImageTags.Image, tag ?? InfluxDbContainerImageTags.Tag)
            .WithImageRegistry(InfluxDbContainerImageTags.Registry);
        
        if (port is not null)
        {
            resourceBuilder.WithEndpoint(port: port, targetPort: 8086);
        }

        return resourceBuilder;
    }
}