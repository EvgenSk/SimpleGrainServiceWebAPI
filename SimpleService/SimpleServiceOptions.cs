using Microsoft.Extensions.Options;

namespace SimpleService
{
    public class SimpleServiceOptions : IOptions<SimpleServiceOptions>
    {
        public string StringOption { get; set; }

        SimpleServiceOptions IOptions<SimpleServiceOptions>.Value => this;
    }
}