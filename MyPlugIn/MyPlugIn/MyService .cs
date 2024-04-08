using Microsoft.Extensions.Logging;
using MyPlugIn.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace MyPlugInCode
{
    public class MyService : ITransientDependency
    {
        private readonly ILogger<MyService> _logger;
        private readonly IContentRepository _contentsAppService;
        public MyService(ILogger<MyService> logger , IContentRepository contentsAppService)
        {
            _logger = logger;
            _contentsAppService = contentsAppService;
        }

        public async void Initialize()
        {
            _logger.LogInformation("MyService has been initialized");
           var t =await _contentsAppService.GetListAsync();
            _logger.LogInformation("MyService has been initializedt" + t.Count);

        }
    }
}
