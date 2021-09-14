using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RefactoringTest.ProductService.Services;
using RefactoringTest.ProductService.Repositories;
using RefactoringTest.ProductService.Model;
using RefactoringTest.ProductService.ValidationRules.FluentValidation;
using RefactoringTest.ProductService.Constants;
using Microsoft.Extensions.DependencyInjection;
using RefactoringTest.ProductService.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace RefactoringTest.ProductService
{
    public class ProductService : IProductService
    {
       
        private readonly ILogger<ProductService> _logger;

      //  private readonly IProductRepository _productRepository;

        private readonly IBrandService _brandService;
      
        public ProductService() : this(ServiceLocator.ServiceProvider.GetService<ILogger<ProductService>>(typeof(ILogger<ProductService>)), ServiceLocator.ServiceProvider.GetService<IBrandService>(typeof(IBrandService)))
        {
            /*
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<IProductService, ProductService>();
            var serviceProvider = services.BuildServiceProvider();
         //   ServiceLocator.Initialize(serviceProvider.GetService<IServiceProviderProxy>());
            _logger = ServiceLocator.ServiceProvider.GetService<ILogger<ProductService>>(typeof(ILogger<ProductService>));
            _brandService = ServiceLocator.ServiceProvider.GetService<IBrandService>(typeof(IBrandService));


            var configuration = new ConfigurationBuilder();

            services.AddSingleton(c => configuration);
            */
        }


        /*
        public ProductService(ILogger<ProductService> logger, IProductRepository productRepository, IBrandService brandService)
        {
            
            _logger = logger;
           _productRepository = productRepository;
            _brandService = brandService;
           
        }
        */


        public ProductService(ILogger<ProductService> logger, IBrandService brandService)
        {
            _logger = logger;
            _brandService = brandService;
        }

        private readonly IEnumerable<int> _allAvailableIds = Enumerable.Range(0, 100000000);
      
        private const string _usedIdsFileName = "used_ids.txt";
        
        public bool AddProductAsync(string productName, string productDescirption, decimal price, int quantity, string brandName, string promotion)
        {

            _logger.LogInformation("Product Service called.");


            var product = new Product
            {
                ProductName = productName,
                ProductDescription = productDescirption,
                Price = price,
                Quantity = quantity,
                BrandName = brandName,
                Promotion = promotion
            };

            var validator = new ProductValidator();

            var validationResult = validator.Validate(product);

            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.Errors.Select(e => new {
                    Field = e.PropertyName,
                    Error = e.ErrorMessage
                }).ToString());
            }

            var isBrandAllowed = _brandService.IsBrandAllowed(product.BrandName).Result;

            if (!isBrandAllowed)
            {
                throw new ArgumentException(Messages.BrandIsNotAllowed);
            }

            price = CalculatePromotionPrice(promotion, price);

           var id = GetAvailableId();


           ProductRepository.AddProduct(id, productName, productDescirption, price, quantity, brandName);

            /*
            await _productRepository.AddAsync(product);
            */

            return true;
        }



        private decimal CalculatePromotionPrice(string promotion, decimal price)
        {
            if (promotion == "5PERCENTOFF")
                price = price - price * 0.05m;
            else if (promotion == "10PERCENTOFF")
                price = price - price * 0.1m;
            else if (promotion == "20PERCENTOFF")
                price = price - price * 0.2m;
            else if (!string.IsNullOrEmpty((promotion)))
                throw new ArgumentException("Invalid promotion specified");

            return price;
        }

        private int GetAvailableId()
        {
            var usedIds = File.ReadAllLines(_usedIdsFileName).Select(int.Parse);
            var id = _allAvailableIds.FirstOrDefault(x => usedIds.All(i => i != x));

            if (id == 0)
                throw new Exception("No available ids left");

            return id;
        }
    }
}