using GrainInterfaces;
using Grains;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orleans;
using Orleans.Hosting;
using OrleansSimple;
using SimpleGrainService;
using SimpleService;
using System.Threading.Tasks;

namespace SimpleGrainServiceWebAPI
{
    public static class OrleansHostingHelper
    {
        private static ISiloHost BuildSiloHost(IConfiguration configuration)
        {
            var simpleServiceOptionsSection = configuration.GetSection("SimpleService");

            return new SiloHostBuilder()
                .Configure<SimpleServiceOptions>(simpleServiceOptionsSection)
                .AddGrainService<SimpleGrainService.SimpleGrainService>()
                .ConfigureServices(s =>
                {
                    s.AddSingleton<ISimpleGrainService, SimpleGrainService.SimpleGrainService>();
                    s.AddSingleton<ISimpleGrainServiceClient, SimpleGrainServiceClient>();
                    s.AddSingleton<ISimpleService, SimpleService.SimpleService>();
                })
                .UseLocalhostClustering()
                .ConfigureApplicationParts(parts =>
                {
                    parts.AddApplicationPart(typeof(IGrainWithSimpleService).Assembly).WithReferences();
                    parts.AddApplicationPart(typeof(GrainWithSimpleService).Assembly).WithReferences();
                })
                .Build();
        }

        public static async Task<IServiceCollection> AddOrleansClusterClient(this IServiceCollection services, IConfiguration configuration)
        {
            ISiloHost siloHost = BuildSiloHost(configuration);
            await siloHost.StartAsync();
            var client = siloHost.Services.GetRequiredService<IClusterClient>();
            return
                services
                .AddSingleton<ISiloHost>(siloHost)      // in order to be disposed correctly
                .AddSingleton<IClusterClient>(client)
                .AddSingleton<IGrainFactory>(client);
        }
    }
}
