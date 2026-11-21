This is a Blazor Server app template with
- EF Core with Postgres in the [Database](Database/) project
- [Radzen](https://blazor.radzen.com/) components
- [Bootstrap 6](https://getbootstrap.com/)

When using for the first time, remove the `UserSecretsId` element from the `Web.csproj` file so it can regenerate.

# About the database

The [DbFactory](Database/DbFactory.cs) class is needed for migrations. It has a `GetConnectionInfo` method used both by the `DbFactory` class and Web app startup. In particular, it looks at the startup `string[] args` followed by a `ConnectionName` config value. If neither is set, it uses a connection string called `Local`.

There's a [DbConnectionInfo](Web/DbConnectionInfo.cs) class that has the active connection name set in startup. You can inject this in any component to show the active db connection name.

Note also that I use a DbContextFactory instead of plain DbContext since that works better for Blazor. If you use an ordinary DbContext, you'll get a lot of threading errors. Using a DbContextFactory instead lets you control the DbContext lifecycle explicitly within your own `using` blocks.

# Bramblelog

I have my own cloud logging app called [Bramblelog](https://bramblelog.dev). The client package is included but not configured. You can enable it by adding a `Bramblelog` section to your configuration, either in dev secrets or appsettings.json. Example:

```json
"Bramblelog": {
  "ApplicationName": "YourAppName",
  "WebhookSecret": "whatever",
  "APIKey": "<your key>"
}
```

Bramblelog works like any `ILogger<T>` provider.

# Known Issues
- Dark mode support doesn't work in the Account/ pages.
- Account/Passkeys page doesn't work. Seems to be issue with ASP.NET Identity EF Core migration -- the `AspNetUserPasskeys` table is not created.