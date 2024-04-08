using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MyPlugIn.Contents
{
    public partial interface IContentRepository : IRepository<Content, Guid>
    {

        Task DeleteAllAsync(
            string? filterText = null,
            string? name = null,
            string? value = null,
            CancellationToken cancellationToken = default);
        Task<List<Content>> GetListAsync(
                    string? filterText = null,
                    string? name = null,
                    string? value = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            string? value = null,
            CancellationToken cancellationToken = default);
    }
}