using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using RefactoringTest.ProductService.Model.Dtos;
using RefactoringTest.ProductService.Services;

namespace RefactoringTest.ProductService.Functions
{
    public class ProductFunctions
    {
        private readonly IProductService _productService;

     
        private readonly ILogger<ProductFunctions> _logger; 
        public ProductFunctions(IProductService productService,ILogger<ProductFunctions> logger)
        {
            _productService = productService;
          
            _logger = logger;
        }

        [FunctionName("AddProduct")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] ProductRequest req, ILogger log)
        {
            _logger.LogInformation("Add product request received");

      
            var addProductResult =  _productService.AddProductAsync(req.ProductName,req.ProductDescription,req.Price,req.Quantity,req.BrandName,req.Promotion);

            return new OkObjectResult(addProductResult);
        }
    }
}
