namespace RefactoringTest.ProductService.Model
{
    public class Product: EntityBase
    {
        public string ProductName {get;set;}
        public string ProductDescription {get;set;}
        public int Quantity {get;set;}
        public decimal Price {get;set;}
        public string BrandName {get;set;}
        public string Promotion { get; set; }
    }
}