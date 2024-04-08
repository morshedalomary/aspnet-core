using Volo.Abp.Application.Dtos;
using System;

namespace MyPlugIn.Contents
{
    public abstract class GetContentsInputBase : PagedAndSortedResultRequestDto
    {

        public string? FilterText { get; set; }

        public string? Name { get; set; }
        public string? Value { get; set; }

        public GetContentsInputBase()
        {

        }
    }
}