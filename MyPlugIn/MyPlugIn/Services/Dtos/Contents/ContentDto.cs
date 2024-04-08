using System;
using System.Collections.Generic;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace MyPlugIn.Contents
{
    public abstract class ContentDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string? Name { get; set; }
        public string? Value { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;

    }
}