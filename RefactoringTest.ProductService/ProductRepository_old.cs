using Dapper;
using Microsoft.Data.SqlClient;

namespace RefactoringTest.ProductService
{
    public static class ProductRepository
    {
        public static void AddProduct(int id, string productName, string productDescription, decimal price, int quantity, string brandName)
        {
            using(var sqlConnection = new SqlConnection("localhost:7001"))
            {
                var sql = @"INSERT INTO Product VALUES (@id, @productName, @prodDescription, @price, @quantity, @brandName)";
                sqlConnection.Execute(sql, new {id, productName, productDescription, price, quantity, brandName});
            }
        }
    }
}