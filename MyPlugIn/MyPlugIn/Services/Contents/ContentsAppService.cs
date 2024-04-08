using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using MyPlugIn.Permissions;
using MyPlugIn.Contents;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using MyPlugIn.Shared;

namespace MyPlugIn.Contents
{
    [RemoteService(IsEnabled = false)]
    public abstract class ContentsAppServiceBase : ApplicationService
    {
        protected IDistributedCache<ContentExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        protected IContentRepository _contentRepository;
        protected ContentManager _contentManager;

        public ContentsAppServiceBase(IContentRepository contentRepository, ContentManager contentManager, IDistributedCache<ContentExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _contentRepository = contentRepository;
            _contentManager = contentManager;
        }

        public virtual async Task<PagedResultDto<ContentDto>> GetListAsync(GetContentsInput input)
        {
            var totalCount = await _contentRepository.GetCountAsync(input.FilterText, input.Name, input.Value);
            var items = await _contentRepository.GetListAsync(input.FilterText, input.Name, input.Value, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ContentDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Content>, List<ContentDto>>(items)
            };
        }

        public virtual async Task<ContentDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Content, ContentDto>(await _contentRepository.GetAsync(id));
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            await _contentRepository.DeleteAsync(id);
        }

        public virtual async Task<ContentDto> CreateAsync(ContentCreateDto input)
        {

            var content = await _contentManager.CreateAsync(
            input.Name, input.Value
            );

            return ObjectMapper.Map<Content, ContentDto>(content);
        }

        public virtual async Task<ContentDto> UpdateAsync(Guid id, ContentUpdateDto input)
        {

            var content = await _contentManager.UpdateAsync(
            id,
            input.Name, input.Value, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Content, ContentDto>(content);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ContentExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _contentRepository.GetListAsync(input.FilterText, input.Name, input.Value);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Content>, List<ContentExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Contents.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public virtual async Task<MyPlugIn.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new ContentExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new MyPlugIn.Shared.DownloadTokenResultDto
            {
                Token = token
            };
        }
        public virtual async Task DeleteByIdsAsync(List<Guid> contentIds)
        {
            await _contentRepository.DeleteManyAsync(contentIds);
        }

        public virtual async Task DeleteAllAsync(GetContentsInput input)
        {
            await _contentRepository.DeleteAllAsync(input.FilterText, input.Name, input.Value);
        }
    }
}