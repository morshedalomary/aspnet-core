using MasterProject.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace MasterProject.Permissions;

public class MasterProjectPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(MasterProjectPermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(MasterProjectPermissions.MyPermission1, L("Permission:MyPermission1"));

   
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MasterProjectResource>(name);
    }
}