using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CostCalculator.Application.Materials.Queries.GetMaterialById
{
    public class GetMaterialByIdQueryValidator : AbstractValidator<GetMaterialByIdQuery>
    {
        public GetMaterialByIdQueryValidator()
        {
            RuleFor(m => m.Id).NotEmpty().WithMessage("Material ID is required.");
        }
    }
}
