using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Concurrency;
using Orleans.Core;
using Orleans.Runtime;
using SimpleService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGrainService
{
    [Reentrant]
    public class SimpleGrainService : GrainService, ISimpleGrainService
    {
        readonly IGrainFactory GrainFactory;
        readonly ISimpleService simpleService;

        public SimpleGrainService(IServiceProvider services, IGrainIdentity id, Silo silo, ILoggerFactory loggerFactory, IGrainFactory grainFactory)
            : base(id, silo, loggerFactory)
        {
            GrainFactory = grainFactory;
            simpleService = services.GetRequiredService<ISimpleService>();
        }

        public Task<string> GetStringOption() =>
            simpleService.GetStringOption();
    }
}
