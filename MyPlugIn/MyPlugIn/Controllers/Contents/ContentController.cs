using Asp.Versioning;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using MyPlugIn.Contents;
using Volo.Abp.Content;
using MyPlugIn.Shared;

namespace MyPlugIn.Contents
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Content")]
    [Route("api/app/plugin/contents")]

    public abstract class ContentControllerBase : AbpController
    {
        protected IContentsAppService _contentsAppService;

        public ContentControllerBase(IContentsAppService contentsAppService)
        {
            _contentsAppService = contentsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<ContentDto>> GetListAsync(GetContentsInput input)
        {
            return _contentsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ContentDto> GetAsync(Guid id)
        {
            return _contentsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<ContentDto> CreateAsync(ContentCreateDto input)
        {
            return _contentsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<ContentDto> UpdateAsync(Guid id, ContentUpdateDto input)
        {
            return _contentsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _contentsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(ContentExcelDownloadDto input)
        {
            return _contentsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public virtual Task<MyPlugIn.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _contentsAppService.GetDownloadTokenAsync();
        }
        [HttpDelete]
        [Route("")]
        public virtual Task DeleteByIdsAsync(List<Guid> contentIds)
        {
            return _contentsAppService.DeleteByIdsAsync(contentIds);
        }

        [HttpDelete]
        [Route("all")]
        public virtual Task DeleteAllAsync(GetContentsInput input)
        {
            return _contentsAppService.DeleteAllAsync(input);
        }
    }
}