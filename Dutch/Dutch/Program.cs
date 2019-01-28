using System;
using Dutch.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Dutch
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var host = BuildWebHost(args);

            //RunSeeding(host);

            //host.Run();


            var i = CreateWebHostBuilder(args).Build();
            
            RunSeeding(i);
            i.Run();//.Run();
        }

      

       

        private static void RunSeeding(IWebHost host)
        {
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();

            using (var scope = scopeFactory.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetService<DutchSeeder>();
                seeder.SeedAsync().Wait();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(SetupConfiguration)
                .UseStartup<Startup>();

        private static void SetupConfiguration(WebHostBuilderContext ctx, IConfigurationBuilder builder)
        {
            // removing the default configuration options
            builder.Sources.Clear();
            builder.AddJsonFile("config.Json", false, true)
                .AddEnvironmentVariables();

        }
    }
}
