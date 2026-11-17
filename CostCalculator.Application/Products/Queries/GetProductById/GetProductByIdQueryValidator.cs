using CostCalculator.Application.Materials.Queries.GetMaterialById;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CostCalculator.Application.Products.Queries.GetProductById
{
    public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdQueryValidator()
        {
            RuleFor(p => p.Id).NotEmpty().WithMessage("Product ID is required.");
        }
    }
}
