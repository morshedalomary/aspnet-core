using Microsoft.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;

namespace MyPlugIn.Data;

public class MyPlugInEFCoreDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public MyPlugInEFCoreDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the MyPlugInDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<MyPlugInDbContext>()
            .Database
            .MigrateAsync();
    }
}
