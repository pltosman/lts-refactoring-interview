using System;
using Microsoft.EntityFrameworkCore;
using RefactoringTest.ProductService.Model;

namespace RefactoringTest.ProductService.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

    }
}
