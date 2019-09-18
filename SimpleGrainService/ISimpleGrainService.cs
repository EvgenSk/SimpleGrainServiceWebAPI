﻿using Orleans.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGrainService
{
    public interface ISimpleGrainService : IGrainService
    {
        Task<string> GetStringOption();
    }
}
