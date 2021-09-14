using System;
namespace RefactoringTest.ProductService.Model.Dtos
{
    public class ProductRequest
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string BrandName { get; set; }
        public string Promotion { get; set; }
    }
}
