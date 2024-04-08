using MyPlugIn.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace MyPlugIn.Permissions;

public class MyPlugInPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(MyPlugInPermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(MyPlugInPermissions.MyPermission1, L("Permission:MyPermission1"));

        var contentPermission = myGroup.AddPermission(MyPlugInPermissions.Contents.Default, L("Permission:Contents"));
        contentPermission.AddChild(MyPlugInPermissions.Contents.Create, L("Permission:Create"));
        contentPermission.AddChild(MyPlugInPermissions.Contents.Edit, L("Permission:Edit"));
        contentPermission.AddChild(MyPlugInPermissions.Contents.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MyPlugInResource>(name);
    }
}