using Volo.Abp.Application.Dtos;

namespace MyPlugIn.Contents
{
    public partial interface IContentsAppService
    {
        //Write your custom code here...

        Task<PagedResultDto<ContentDto>> GetAll(GetContentsInput input);
        Task<ContentDto> GetCMSContent(Guid id);
        Task<ContentDto> InsertOrUpdateCMSContent(ContentCreateUpdateDto contentCreateUpdateDto);
    }
}