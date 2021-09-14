using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RefactoringTest.ProductService.Constants;
using RefactoringTest.ProductService.Helpers;
using RefactoringTest.ProductService.Infrastructure;
using RestSharp;

namespace RefactoringTest.ProductService.Services
{
    public class BrandService : IBrandService
    {

        private readonly ILogger<BrandService> _logger;
        private readonly RestClient _restClient;
        private readonly AppSettings _settings;


        public BrandService() :this(ServiceLocator.ServiceProvider.GetService<ILogger<BrandService>>(typeof(ILogger<BrandService>)), ServiceLocator.ServiceProvider.GetService<IOptions<AppSettings>>(typeof(IOptions<AppSettings>)))
        {

       var services= GetServiceLocator<Startup>();

            _logger = services.get<
            /*
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<IBrandService, BrandService>();
            var serviceProvider = services.BuildServiceProvider();

            ServiceLocator.Initialize(serviceProvider.GetService<IServiceProviderProxy>());

            _logger = (ILogger<BrandService>)serviceProvider.GetService(typeof(ILogger<BrandService>));
            var options = (IOptions<AppSettings>)serviceProvider.GetService(typeof(IOptions<AppSettings>));
            _settings = options.Value;
            */
        }

        public BrandService(ILogger<BrandService> logger, IOptions<AppSettings> options)
        {
            _logger = logger;
            _settings = options.Value;
            _restClient = new RestClient(_settings.BrandServiceUrl);
        }

        public async Task<bool> IsBrandAllowed(string brandName)
        {
            var brandResponse = false;

            if (string.IsNullOrEmpty(brandName))
            {
                _logger.LogError(Messages.BrandNameNotbeNull);
            }

            try
            {
                var request = new RestRequest(Method.GET);
                request.AddParameter("brandName", string.Format(brandName), ParameterType.QueryString);
                request.AddHeader("Content-type", "application/json");

                var cancellationTokenSource = new CancellationTokenSource();

                var response = await _restClient.ExecuteAsync(request, cancellationTokenSource.Token);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonResponse = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var json = jsonResponse.ToString();

                    brandResponse = bool.Parse(json);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

            return brandResponse;
            
        }
    }
}
