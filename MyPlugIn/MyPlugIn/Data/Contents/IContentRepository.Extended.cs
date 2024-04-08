namespace MyPlugIn.Contents
{
    public partial interface IContentRepository
    {
        Task<List<Content>> GetAll(
             string? filterText = null,
             string? name = null,
             string? value = null,
             string? sorting = null,
             int maxResultCount = int.MaxValue,
             int skipCount = 0,
             CancellationToken cancellationToken = default
         );
    }
}