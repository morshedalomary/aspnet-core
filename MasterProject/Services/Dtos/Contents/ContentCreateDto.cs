using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MasterProject.Contents
{
    public abstract class ContentCreateDtoBase
    {
        public string? Name { get; set; }
        public string? Value { get; set; }
    }
}