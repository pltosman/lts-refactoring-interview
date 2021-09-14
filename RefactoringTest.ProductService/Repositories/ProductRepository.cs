using System;
using RefactoringTest.ProductService.Data;
using RefactoringTest.ProductService.Model;
using RefactoringTest.ProductService.Repositories.Base;

namespace RefactoringTest.ProductService.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DataContext dbContext) : base(dbContext)
        {

        }

    }
}
