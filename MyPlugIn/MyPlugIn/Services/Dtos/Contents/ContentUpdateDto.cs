using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace MyPlugIn.Contents
{
    public abstract class ContentUpdateDtoBase : IHasConcurrencyStamp
    {
        public string? Name { get; set; }
        public string? Value { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}