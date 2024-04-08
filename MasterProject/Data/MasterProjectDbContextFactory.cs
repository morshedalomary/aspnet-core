using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MasterProject.Data;

public class MasterProjectDbContextFactory : IDesignTimeDbContextFactory<MasterProjectDbContext>
{
    public MasterProjectDbContext CreateDbContext(string[] args)
    {

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<MasterProjectDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new MasterProjectDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
