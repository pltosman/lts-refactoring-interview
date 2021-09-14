using System;
using RefactoringTest.ProductService.Model;
using RefactoringTest.ProductService.Repositories.Base;

namespace RefactoringTest.ProductService.Repositories
{
    public interface IProductRepository: IRepository<Product>
    {

    }
}
