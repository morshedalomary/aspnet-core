using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace MyPlugIn.Contents
{
    public abstract class ContentManagerBase : DomainService
    {
        protected IContentRepository _contentRepository;

        public ContentManagerBase(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        public virtual async Task<Content> CreateAsync(
        string? name = null, string? value = null)
        {

            var content = new Content(
             GuidGenerator.Create(),
             name, value
             );

            return await _contentRepository.InsertAsync(content);
        }

        public virtual async Task<Content> UpdateAsync(
            Guid id,
            string? name = null, string? value = null, [CanBeNull] string? concurrencyStamp = null
        )
        {

            var content = await _contentRepository.GetAsync(id);

            content.Name = name;
            content.Value = value;

            content.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _contentRepository.UpdateAsync(content);
        }

    }
}