using GrainInterfaces;
using Orleans;
using Orleans.Runtime;
using SimpleGrainService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grains
{
    public class GrainWithSimpleService : Grain, IGrainWithSimpleService
    {
        readonly ISimpleGrainServiceClient SimpleGrainServiceClient;
        public GrainWithSimpleService(IGrainActivationContext context, ISimpleGrainServiceClient client)
        {
            SimpleGrainServiceClient = client;
        }

        public Task<string> GetStringOption() =>
            SimpleGrainServiceClient.GetStringOption();
    }
}
