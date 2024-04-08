namespace MyPlugIn.Contents
{
    public partial interface IContentsAppService
    {
        //Write your custom code here...

        Task<List<ContentDto>> GetAll();
        Task<ContentDto> GetCMSContent(Guid id);
        Task<ContentDto> InsertOrUpdateCMSContent(Guid Id , string Name , string Value);
    }
}