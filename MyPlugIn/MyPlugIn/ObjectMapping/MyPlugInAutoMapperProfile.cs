using System;
using MyPlugIn.Shared;
using Volo.Abp.AutoMapper;
using MyPlugIn.Contents;
using AutoMapper;

namespace MyPlugIn.ObjectMapping;

public class MyPlugInAutoMapperProfile : Profile
{
    public MyPlugInAutoMapperProfile()
    {
        /* Create your AutoMapper object mappings here */

        CreateMap<Content, ContentDto>();
        CreateMap<Content, ContentExcelDto>();
    }
}