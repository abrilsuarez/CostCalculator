using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CostCalculator.Application.Materials.Commands.UpdateMaterial
{
    public class UpdateMaterialCommandValidator : AbstractValidator<UpdateMaterialCommand>
    {
        public UpdateMaterialCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Material's id is required.");

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Material's name is required.")
                .MaximumLength(150).WithMessage("Material's name cannot exceed 150 characters.");

            RuleFor(v => v.UnitCost)
                .GreaterThanOrEqualTo(0).WithMessage("Material's unit cost cannot be negative.");

            RuleFor(v => v.UnitOfMeasure)
                .IsInEnum().WithMessage("The selected unit of measure is not valid.");
        }
    }
}
