using System;

namespace RefactoringTest.LegacyClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var productService = new ProductService.ProductService();
            productService.AddProductAsync("Legacy product", "Very very old", 10, 1, "Old school", null);
            Console.WriteLine("Legacy Product added!");
        }
    }
}
