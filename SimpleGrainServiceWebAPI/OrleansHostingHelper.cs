using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orleans;
using Orleans.Hosting;
using OrleansSimple;
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
                .UseDashboard()
                .UseLocalhostClustering()
                .ConfigureApplicationParts(parts =>
                {
                    parts.AddApplicationPart(typeof(ISimple).Assembly).WithReferences();
                    parts.AddApplicationPart(typeof(SimpleGrain).Assembly).WithReferences();
                })
                //.ConfigureServices((hostBuilderContext, services) =>
                //{
                //    hostBuilderContext.Configuration = configuration;
                //    services
                //    .Configure<SimpleServiceOptions>(simpleServiceOptionsSection)
                //})
                //            .AddGrainService<WordsAPIGrainService>()
                //            .ConfigureServices((hostBuilderContext, services) => {
                //                hostBuilderContext.Configuration = configuration;
                //                services
                //                .Configure<WordsAPIOptions>(simpleServiceOptionsSection)
                //                .Configure<StanfordNLPClientOptions>(stanfordNLPOptionsSection)
                //                .AddWordsAPIClient()
                //                .AddStanfordNLPClient()
                //                .Configure<RedisGrainStorageOptions>(cacheName, redisOptionsSection)
                //                .Configure<MongoDBGrainStorageOptions>(storageName, mongoOptionsSection)
                //                .AddSingleton<IWordsAPIGrainServiceClient, WordsAPIGrainServiceClient>();
                //            })
                //            .Configure<RedisGrainStorageOptions>(redisOptionsSection)
                //            .Configure<MongoDBGrainStorageOptions>(mongoOptionsSection)
                //            .AddRedisGrainStorage(cacheName)
                //            .AddMongoDBGrainStorage(storageName)
                //            .AddCompoundGrainStorage(compoundName, c => {
                //                c.CacheName = cacheName;
                //                c.StorageName = storageName;
                //            })
                //            .AddWordsAPIClient()
                //            .AddStanfordNLPClient()

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
