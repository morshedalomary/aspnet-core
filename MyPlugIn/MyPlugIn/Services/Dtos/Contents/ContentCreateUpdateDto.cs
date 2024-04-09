using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MyPlugIn.Contents
{
    public  class ContentCreateUpdateDto
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }

    }
}