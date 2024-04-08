using Asp.Versioning;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using MyPlugIn.Contents;

namespace MyPlugIn.Contents
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Content")]
    [Route("api/app/plugin/contents")]

    public class ContentController : ContentControllerBase, IContentsAppService
    {
        public ContentController(IContentsAppService contentsAppService) : base(contentsAppService)
        {
        }
        [HttpGet]
        [Route("get-all")]
        public async virtual Task<PagedResultDto<ContentDto>> GetAll(GetContentsInput input)
        {

            List<ContentDto> allContent = new List<ContentDto>();

            return await _contentsAppService.GetAll(input);

           
        }

      


        [HttpGet]
        [Route("get-cms-content/{id}")]
        public  async Task<ContentDto> GetCMSContent(Guid id)
        {
            return await _contentsAppService.GetCMSContent(id);
        }
        [HttpPost]
        [Route("insert-update-content")]

        public async Task<ContentDto> InsertOrUpdateCMSContent(Guid id , string Name , string Value)
        {
            return await _contentsAppService.InsertOrUpdateCMSContent(id, Name , Value);

        }

    }
}