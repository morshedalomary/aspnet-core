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

        var contentPermission = myGroup.AddPermission(MasterProjectPermissions.Contents.Default, L("Permission:Contents"));
        contentPermission.AddChild(MasterProjectPermissions.Contents.Create, L("Permission:Create"));
        contentPermission.AddChild(MasterProjectPermissions.Contents.Edit, L("Permission:Edit"));
        contentPermission.AddChild(MasterProjectPermissions.Contents.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MasterProjectResource>(name);
    }
}