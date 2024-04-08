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
        {
        }

       
    }
}