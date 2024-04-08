using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using MyPlugIn.Data;
using Volo.Abp.Data;

namespace MyPlugIn.Contents
{
    [ConnectionStringName("MySecondaryDb")]

    public class EfCoreContentRepository : EfCoreContentRepositoryBase, IContentRepository
    {
        public EfCoreContentRepository(IDbContextProvider<MyPlugInDbContext> dbContextProvider)
            : base(dbContextProvider)
        { }

        public virtual async Task<List<Content>> GetAll(
     string? filterText = null,
     string? name = null,
     string? value = null,
     string? sorting = null,
     int maxResultCount = int.MaxValue,
     int skipCount = 0,
     CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name, value);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ContentConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }
    }

} 
    
