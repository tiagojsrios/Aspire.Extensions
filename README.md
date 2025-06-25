# Aspire.InfluxDb

## How to use

The simplest way to start using this integration in your .NET Aspire host is by simply adding the `InfluxDb` resource to the `DistributedApplicationBuilder`.

```csharp
IResourceBuilder<InfluxDbResource> resource = builder.AddInfluxDb("InfluxDb");
```

However, the package also has a set of extension methods - located in the `InfluxDbResourceBuilderExtensions` class - which allows you to configure the container on startup. These extension methods will set the correct environment variables based on what you are configuring.

```csharp
IResourceBuilder<InfluxDbResource> resource = builder.AddInfluxDb("InfluxDb")
    .WithSetupMode("setup")
    .WithUsername("admin")
    .WithPassword("password")
    .WithOrganization("organization")
    .WithBucket("bucket");
```

For a more detailed explanation on how you can configure the container via environment variables, you can check InfluxDb's [documentation](https://hub.docker.com/_/influxdb).