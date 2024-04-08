namespace MasterProject.Contents
{
    public static class ContentConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Content." : string.Empty);
        }

    }
}