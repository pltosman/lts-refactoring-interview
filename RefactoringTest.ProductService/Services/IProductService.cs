using System;
using System.Threading.Tasks;

namespace RefactoringTest.ProductService.Services
{
    public interface IProductService
    {
        bool AddProductAsync(string productName, string produtcDescirption, decimal price, int quantity, string brandName, string promotion);
    }
}
