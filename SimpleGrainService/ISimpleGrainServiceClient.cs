using Orleans.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleGrainService
{
    public interface ISimpleGrainServiceClient : IGrainServiceClient<ISimpleGrainService>, ISimpleGrainService
    {
    }
}
