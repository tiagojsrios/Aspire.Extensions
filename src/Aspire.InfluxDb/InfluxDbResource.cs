using Aspire.Hosting.ApplicationModel;

namespace Aspire.InfluxDb;

public sealed class InfluxDbResource(string name, string? entrypoint) : ContainerResource(name, entrypoint);