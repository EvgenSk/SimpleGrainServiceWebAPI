using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleService
{
    public class SimpleService : ISimpleService
    {
        private SimpleServiceOptions Options { get; }

        public SimpleService(IOptions<SimpleServiceOptions> options)
        {
            Options = options.Value;
        }

        public string StringOption => Options.StringOption;

        public Task<string> GetStringOption() => Task.FromResult(Options.StringOption);
    }
}
