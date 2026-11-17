using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CostCalculator.Application.Materials.Commands.CreateMaterial
{
    public class CreateMaterialCommandValidator : AbstractValidator<CreateMaterialCommand>
    {
        public CreateMaterialCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Material name is required.")
                .MaximumLength(150).WithMessage("Material name cannot exceed 150 characters.");

            RuleFor(v => v.UnitCost)
                .GreaterThanOrEqualTo(0).WithMessage("Unit cost cannot be negative.");

            RuleFor(v => v.UnitOfMeasure)
                .IsInEnum().WithMessage("The selected unit of measure is not valid.");
        }
    }
}
