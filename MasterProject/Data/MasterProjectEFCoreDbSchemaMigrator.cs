using Microsoft.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;

namespace MasterProject.Data;

public class MasterProjectEFCoreDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public MasterProjectEFCoreDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the MasterProjectDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<MasterProjectDbContext>()
            .Database
            .MigrateAsync();
    }
}
