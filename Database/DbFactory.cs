using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Database;

public class DbFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    private static IConfiguration Config => new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false)
        .AddUserSecrets("31967f0f-7f6d-4f54-9764-6d2562f41493")
        .Build();

    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var (_, connectionString) = GetConnectionInfo(args, Config);
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseNpgsql(connectionString);
        return new ApplicationDbContext(optionsBuilder.Options);
    }

    public static (string ConnectionName, string ConnectionString) GetConnectionInfo(string[] args, IConfiguration config)
    {
        var connectionName = args.Length > 0 ? args[0] : config.GetValue<string>("ConnectionName") ?? "Local";
        return (connectionName, config.GetConnectionString(connectionName) ?? throw new Exception($"Connection string '{connectionName}' not found"));
    }
}
