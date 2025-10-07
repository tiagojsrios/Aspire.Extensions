using Aspire.Hosting.ApplicationModel;

namespace Aspire.SqlServer;

public sealed record ImportCommandOptions
{
    internal static readonly ImportCommandOptions DefaultInstance = new() { Type = ImportType.Path };
    
    public required ImportType Type { get; set; }
    
    public Func<SqlServerDatabaseResource, ExecuteCommandContext, Task>? BeforeImport { get; set; }

    public Func<SqlServerDatabaseResource, ExecuteCommandContext, Task>? AfterImport { get; set; }

    public enum ImportType
    {
        Path
    }
}