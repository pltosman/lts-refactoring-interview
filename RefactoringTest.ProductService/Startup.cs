using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RefactoringTest.ProductService;
using RefactoringTest.ProductService.Helpers;
using RefactoringTest.ProductService.Infrastructure;
using RefactoringTest.ProductService.Repositories;
using RefactoringTest.ProductService.Repositories.Base;
using RefactoringTest.ProductService.Services;
using Serilog;
using Serilog.Events;
using System;

[assembly: FunctionsStartup(typeof(Startup))]

namespace RefactoringTest.ProductService
{
    public class Startup : FunctionsStartup
    {

        private void RegisterServices(IServiceCollection services)
        {

            var sp = services.BuildServiceProvider();
            ServiceLocator.Initialize(sp.GetService<IServiceProviderProxy>());

            var config = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            services.AddOptions<AppSettings>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection("AppSettings").Bind(settings);
            });

          //  services.AddSingleton<IServiceProviderProxy, HttpContextServiceProviderProxy>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IProductRepository, Repositories.ProductRepository>();

            services.AddSingleton<IProductService, ProductService>();

            services.AddSingleton(c => config);


            var logger = new LoggerConfiguration()
               .MinimumLevel.Verbose()
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.File(
                "Logs/log-.txt",
                shared: true,
                flushToDiskInterval: TimeSpan.FromSeconds(5),
                rollingInterval: RollingInterval.Day)
                .CreateLogger();

         
            services.AddLogging(lb => lb.AddSerilog(logger));

         
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {

           
            RegisterServices(builder.Services);
        }
    }
}
