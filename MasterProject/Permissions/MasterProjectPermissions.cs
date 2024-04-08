namespace MasterProject.Permissions;

public static class MasterProjectPermissions
{
    public const string GroupName = "MasterProject";

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";

    public static class Contents
    {
        public const string Default = GroupName + ".Contents";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}