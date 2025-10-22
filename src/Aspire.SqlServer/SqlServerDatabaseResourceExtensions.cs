#pragma warning disable ASPIREINTERACTION001
using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SqlServer.Dac;

namespace Aspire.SqlServer;

public static class SqlServerDatabaseResourceExtensions
{
    /// <summary>
    /// Method that makes use of Aspire's interaction service by extending the SqlServerDatabaseResource and allowing users to import a .bacpac file into the database.
    /// By default, the extension looks for a .bacpac file in the root of your Aspire.AppHost project. 
    /// </summary>
    public static IResourceBuilder<SqlServerDatabaseResource> WithImportCommand(this IResourceBuilder<SqlServerDatabaseResource> resource, 
        Action<ImportCommandOptions>? configureOptions = null)
    {
        return resource.WithCommand(
            $"{resource.Resource.DatabaseName} import",
            $"Import {resource.Resource.DatabaseName}.bacpac database",
            async ctx =>
            {
                string? bacpacFileLocation = Path.Combine(Directory.GetCurrentDirectory(),
                    $"{resource.Resource.DatabaseName}.bacpac");

                ImportCommandOptions options = ImportCommandOptions.DefaultInstance;
                configureOptions?.Invoke(options);
                
                if (!File.Exists(bacpacFileLocation))
                {
                    switch (options.Type)
                    {
                        case ImportCommandOptions.ImportType.Path:
                            var interactionResult = await PromptForFileLocation(ctx);
                            if (interactionResult.Canceled) { return new ExecuteCommandResult { Success = false }; }
                            bacpacFileLocation = interactionResult.Data?.Value;
                            break;
                    }
                }

                if (string.IsNullOrEmpty(bacpacFileLocation) || !File.Exists(bacpacFileLocation))
                {
                    return new ExecuteCommandResult
                    {
                        Success = false,
                        ErrorMessage = $"Could not find bacpac file at the provided location: {bacpacFileLocation}"
                    };
                }

                options.BeforeImport?.Invoke(resource.Resource, ctx);

                await ImportBacpac(resource.Resource, ctx, bacpacFileLocation);
                
                options.AfterImport?.Invoke(resource.Resource, ctx);
                
                return new ExecuteCommandResult
                {
                    Success = true
                };
            }
        );
    }

    private static async Task<InteractionResult<InteractionInput>> PromptForFileLocation(ExecuteCommandContext context)
    {
        IInteractionService interaction = context.ServiceProvider.GetRequiredService<IInteractionService>();

        InteractionResult<InteractionInput> interactionResult = await interaction.PromptInputAsync(
            title: "Import SQL database",
            message: "By default, this extension looks for a .bacpac file in the root of your Aspire.AppHost project. " +
                     "When the file is not found, this prompt is used as a fallback and it allows you to specify the absolute path of the .bacpac file used for the import.",
            input: new InteractionInput
            {
                Name = "Bacpac file location input",
                Label = ".bacpac file location",
                InputType = InputType.Text,
                Required = true
            },
            options: new InputsDialogInteractionOptions
            {
                PrimaryButtonText = "Import"
            }
        );

        return interactionResult;
    }

    private static async Task ImportBacpac(SqlServerDatabaseResource resource, ExecuteCommandContext context, string fileLocation)
    {
        string? connectionString = await resource.ConnectionStringExpression.GetValueAsync(context.CancellationToken);

        DacServices dac = new(connectionString);
        dac.ProgressChanged += (_, e) =>
        {
            ResourceLoggerService resourceLoggerService =
                context.ServiceProvider.GetRequiredService<ResourceLoggerService>();
            ILogger logger = resourceLoggerService.GetLogger(resource.Name);
            logger.LogInformation("{OperationId} | {Status} | {Message}", e.OperationId, e.Status, e.Message);
        };

        using BacPackage bacPackage = BacPackage.Load(fileLocation);
        dac.ImportBacpac(bacPackage, resource.DatabaseName, context.CancellationToken);
    }
}