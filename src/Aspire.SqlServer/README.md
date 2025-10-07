# SqlServer.Aspire.Extensions

This package contains extension methods that improve the developer experience when working with SQL Server resources on .NET Aspire.

```
WithImportCommand(this IResourceBuilder<SqlServerDatabaseResource> resource, Action<ImportCommandOptions>? configureOptions = null)
```

The `WithImportCommand` extends the Aspire dashboard by adding a button on the SQL server database resource that allows you to import a .bacpac file.

By default, the method will look for a .bacpac file that matches the name of your database resource. This file needs to be located in the root directory of your Aspire.AppHost project. 

If the file is not found, then the fallback behavior is the value specified on the `ImportCommandOptions.Type` property, which currently it can only be `Path`. This means that you will be prompted to specify the absolute path of the file.

Through the `configureOptions` parameter, you can also run custom code before or after the import. This can be useful, for example, to run custom SQL scripts to alter the database before the import.
