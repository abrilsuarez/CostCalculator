using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CostCalculator.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Product ID is required.");
        }
    }
}
