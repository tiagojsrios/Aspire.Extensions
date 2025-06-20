namespace Aspire.InfluxDb;

/// <summary>
/// Constants used to configure the InfluxDb resource
/// </summary>
internal static class InfluxDbContainerImageTags
{
    /// <summary>
    /// Register where the InfluxDb container image is hosted
    /// </summary>
    /// <remarks>docker.io</remarks>
    public const string Registry = "docker.io";

    /// <summary>
    /// Container image name
    /// </summary>
    /// <remarks>https://hub.docker.com/_/influxdb</remarks>
    public const string Image = "influxdb";

    /// <summary>
    /// Container image tag
    /// </summary>
    public const string Tag = "latest";
}
