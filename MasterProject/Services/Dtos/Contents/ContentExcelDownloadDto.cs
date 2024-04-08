using Volo.Abp.Application.Dtos;
using System;

namespace MasterProject.Contents
{
    public abstract class ContentExcelDownloadDtoBase
    {
        public string DownloadToken { get; set; } = null!;

        public string? FilterText { get; set; }

        public string? Name { get; set; }
        public string? Value { get; set; }

        public ContentExcelDownloadDtoBase()
        {

        }
    }
}