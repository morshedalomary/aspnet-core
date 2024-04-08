using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using MasterProject.Data;

namespace MasterProject.Contents
{
    public class EfCoreContentRepository : EfCoreContentRepositoryBase, IContentRepository
    {
        public EfCoreContentRepository(IDbContextProvider<MasterProjectDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}