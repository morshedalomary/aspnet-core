using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MyPlugIn.Data;

public class MyPlugInDbContextFactory : IDesignTimeDbContextFactory<MyPlugInDbContext>
{
    public MyPlugInDbContext CreateDbContext(string[] args)
    {

        var configuration = BuildConfiguration();
        var builder = new DbContextOptionsBuilder<MyPlugInDbContext>()
         .UseSqlServer(configuration.GetConnectionString("MySecondaryDb"));
        return new MyPlugInDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
