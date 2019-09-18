using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Core;
using Orleans.Runtime;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGrainService
{
    public class SimpleGrainService : GrainService, ISimpleGrainService
    {
        readonly IGrainFactory GrainFactory;
        readonly ISimpleGrainService simpleGrainService;

        public SimpleGrainService(IServiceProvider services, IGrainIdentity id, Silo silo, ILoggerFactory loggerFactory, IGrainFactory grainFactory)
            : base(id, silo, loggerFactory)
        {
            GrainFactory = grainFactory;
            simpleGrainService = services.GetRequiredService<ISimpleGrainService>();
        }

        public Task<string> GetStringOption() =>
            simpleGrainService.GetStringOption();
    }
}
