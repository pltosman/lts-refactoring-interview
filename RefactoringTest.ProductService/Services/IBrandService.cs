using System;
using System.Threading.Tasks;

namespace RefactoringTest.ProductService.Services
{
    public interface IBrandService
    {
        Task<bool> IsBrandAllowed(string brandName);
    }
}
