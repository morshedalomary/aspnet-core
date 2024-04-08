using System;
using MasterProject.Shared;
using Volo.Abp.AutoMapper;
using MasterProject.Contents;
using AutoMapper;

namespace MasterProject.ObjectMapping;

public class MasterProjectAutoMapperProfile : Profile
{
    public MasterProjectAutoMapperProfile()
    {
        /* Create your AutoMapper object mappings here */

        CreateMap<Content, ContentDto>();
        CreateMap<Content, ContentExcelDto>();
    }
}