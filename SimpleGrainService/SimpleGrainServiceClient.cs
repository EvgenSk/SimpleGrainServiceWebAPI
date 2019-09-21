using Orleans.Runtime.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGrainService
{
    public class SimpleGrainServiceClient : GrainServiceClient<ISimpleGrainService>, ISimpleGrainServiceClient
    {
        public SimpleGrainServiceClient(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        public Task<string> GetStringOption() =>
            GrainService.GetStringOption();
    }
}
