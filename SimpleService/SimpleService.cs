using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleService
{
    public class SimpleService : ISimpleService
    {
        private SimpleServiceOptions Options { get; }

        public SimpleService(SimpleServiceOptions options)
        {
            Options = options;
        }

        public string StringOption => Options.StringOption;

        public Task<string> GetStringOption() => Task.FromResult(Options.StringOption);
    }
}
