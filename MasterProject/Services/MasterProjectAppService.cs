using MasterProject.Localization;
using Volo.Abp.Application.Services;

namespace MasterProject.Services;

/* Inherit your application services from this class. */
public abstract class MasterProjectAppService : ApplicationService
{
    protected MasterProjectAppService()
    {
        LocalizationResource = typeof(MasterProjectResource);
    }
}