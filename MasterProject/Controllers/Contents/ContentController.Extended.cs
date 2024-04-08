using Asp.Versioning;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using MasterProject.Contents;

namespace MasterProject.Contents
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Content")]
    [Route("api/app/contents")]

    public class ContentController : ContentControllerBase, IContentsAppService
    {
        public ContentController(IContentsAppService contentsAppService) : base(contentsAppService)
        {
        }
    }
}