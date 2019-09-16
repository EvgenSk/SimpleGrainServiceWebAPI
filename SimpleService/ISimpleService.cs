using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleService
{
    public interface ISimpleService
    {
        string StringOption { get; }
        Task<string> GetStringOption();
    }
}
