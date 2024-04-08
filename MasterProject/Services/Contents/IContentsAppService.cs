using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using MasterProject.Shared;

namespace MasterProject.Contents
{
    public partial interface IContentsAppService : IApplicationService
    {

        Task<PagedResultDto<ContentDto>> GetListAsync(GetContentsInput input);

        Task<ContentDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<ContentDto> CreateAsync(ContentCreateDto input);

        Task<ContentDto> UpdateAsync(Guid id, ContentUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(ContentExcelDownloadDto input);

        Task<MasterProject.Shared.DownloadTokenResultDto> GetDownloadTokenAsync(); Task DeleteByIdsAsync(List<Guid> contentIds);

        Task DeleteAllAsync(GetContentsInput input);
    }
}