using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CostCalculator.Application.Materials.Commands.DeleteMaterial
{
    public class DeleteMaterialCommandValidator : AbstractValidator<DeleteMaterialCommand>
    {
        public DeleteMaterialCommandValidator()
        {
            RuleFor(m => m.Id)
                .NotEmpty().WithMessage("Material ID is required.");
        }
    }
}
