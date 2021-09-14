using System;
using FluentValidation.TestHelper;
using RefactoringTest.ProductService.Model;
using RefactoringTest.ProductService.ValidationRules.FluentValidation;
using Xunit;

namespace RefactoringTest.ProductService.Tests
{
    public class ProductServiceTests
    {
        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        public void FieldsEmpty_ReturnsValidationErrors(string productName, string productDescription)
        {
            var validator = new ProductValidator();

            validator.ShouldHaveValidationErrorFor(x => x.ProductName, productName);
            validator.ShouldHaveValidationErrorFor(x => x.ProductDescription, productDescription);
           
        }

        [Fact]
        public void Price_Zero_HasValidationError()
        {
            var validator = new ProductValidator();
            var dto = new Product { Price = 0 };

            var validationResults = validator.TestValidate(dto);

            validationResults.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Fact]
        public void Price_One_DoesNotHaveValidationError()
        {
            var validator = new ProductValidator();
            var dto = new Product { Price = 1 };

            var validationResults = validator.TestValidate(dto);

            validationResults.ShouldNotHaveValidationErrorFor(x => x.Price);
        }




    }
}
