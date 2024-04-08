using MyPlugIn.Localization;
using Volo.Abp.Application.Services;

namespace MyPlugIn.Services;

/* Inherit your application services from this class. */
public abstract class MyPlugInAppService : ApplicationService
{
    protected MyPlugInAppService()
    {
        LocalizationResource = typeof(MyPlugInResource);
    }
}