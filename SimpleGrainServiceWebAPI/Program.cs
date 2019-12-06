using GrainInterfaces;
using Grains;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Hosting;
using SimpleGrainService;
using SimpleService;
using System.Threading.Tasks;

namespace SimpleGrainServiceWebAPI
{
	public class Program
	{
		public static void Main(string[] args) =>
			CreateHostBuilder(args).Build().Run();

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
			.ConfigureServices((context, services) =>
			{
				var simpleServiceSection = context.Configuration.GetSection("SimpleService");
				services.Configure<SimpleServiceOptions>(simpleServiceSection);
				services.AddSingleton<ISimpleService, SimpleService.SimpleService>();
			})
			.ConfigureWebHostDefaults(webBuilder =>
			{
				webBuilder.UseStartup<Startup>();
			})
			.UseOrleans((context, siloBuilder) =>
			{
				var simpleServiceSection = context.Configuration.GetSection("SimpleService");
				siloBuilder
				.Configure<SimpleServiceOptions>(simpleServiceSection)
				.UseLocalhostClustering()
				.AddGrainService<SimpleGrainService.SimpleGrainService>()
				.ConfigureServices(services =>
				{
					services.Configure<SimpleServiceOptions>(simpleServiceSection);
					services.AddSingleton<ISimpleService, SimpleService.SimpleService>();
					services.AddSingleton<ISimpleGrainServiceClient, SimpleGrainServiceClient>();
					services.AddSingleton<ISimpleGrainService, SimpleGrainService.SimpleGrainService>();
					services.AddGrainService<SimpleGrainService.SimpleGrainService>();
				})
				.ConfigureApplicationParts(parts =>
				{
					parts.AddApplicationPart(typeof(IGrainWithSimpleService).Assembly).WithReferences();
					parts.AddApplicationPart(typeof(GrainWithSimpleService).Assembly).WithReferences();
					parts.AddApplicationPart(typeof(ISimpleGrainService).Assembly).WithReferences();
				});
			});
	}
}
