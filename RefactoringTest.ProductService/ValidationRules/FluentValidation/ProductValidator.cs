using System;
using FluentValidation;
using RefactoringTest.ProductService.Constants;
using RefactoringTest.ProductService.Model;

namespace RefactoringTest.ProductService.ValidationRules.FluentValidation
{
    public class ProductValidator: AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(product => product.ProductName).NotEmpty().WithMessage(Messages.ProductNameNotbeNull);
            RuleFor(product => product.ProductDescription).NotEmpty().WithMessage(Messages.ProductDescriptionNotbeNull);

            RuleFor(product => product.Price).GreaterThan(0);
            RuleFor(product => product.Quantity).GreaterThan(0);


        }
    }
    
}


