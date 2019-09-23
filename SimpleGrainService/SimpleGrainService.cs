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
        readonly ISimpleService SimpleService;

        public SimpleGrainService(ISimpleService simpleService, IGrainIdentity id, Silo silo, ILoggerFactory loggerFactory)
            : base(id, silo, loggerFactory)
        {
            SimpleService = simpleService;
        }

        public Task<string> GetStringOption() =>
            SimpleService.GetStringOption();
    }
}
