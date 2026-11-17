using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CostCalculator.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Product Id is required."); 
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(200).WithMessage("Product name must not exceed 200 characters.");

            RuleFor(v => v.Description)
                .MaximumLength(1000).WithMessage("Product description must not exceed 1000 characters.");

            RuleFor(v => v.LaborCost)
                .GreaterThanOrEqualTo(0).WithMessage("Labor cost cannot be negative.");

            RuleFor(v => v.ProfitMarginPercentage)
                .GreaterThanOrEqualTo(0).WithMessage("Profit margin percentage cannot be negative.");

            RuleFor(v => v.RecipeItems)
                .NotEmpty().WithMessage("At least one recipe item is required.");

            RuleForEach(v => v.RecipeItems).ChildRules(recipeItem =>
            {
                recipeItem.RuleFor(i => i.MaterialId)
                    .NotEmpty().WithMessage("Material ID is required.");
                recipeItem.RuleFor(i => i.QuantityRequired)
                    .GreaterThan(0).WithMessage("Quantity required must be greater than zero.");
            });
        }
    }
}
