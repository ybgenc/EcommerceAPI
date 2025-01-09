using EcommerceAPI.Application.ViewModels.Products;
using EcommerceAPI.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EcommerceAPI.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VMCreateProduct>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                                .NotNull()
                                    .WithMessage("Product name cannot be empty.")
                                .MaximumLength(200)
                                .MinimumLength(2)
                                    .WithMessage("Product name should be between 2 and 200 characters.");
            RuleFor(x => x.Stock).NotEmpty()
                                 .NotNull()
                                    .WithMessage("Product stock cannot be empty.")
                                 .Must(y => y >= 0 )
                                    .WithMessage("Product stock cannot be negative!.");
            RuleFor(x => x.Price).NotEmpty()
                                 .NotNull()
                                    .WithMessage("Product price cannot be empty.")
                                 .Must(y => y > 0)
                                    .WithMessage("Product price cannot be 0 or negative!");

        }
    }
}
