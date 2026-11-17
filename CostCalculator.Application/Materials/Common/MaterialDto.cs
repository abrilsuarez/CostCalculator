using CostCalculator.Domain.Entities;
using CostCalculator.Domain.Enums;

namespace CostCalculator.Application.Materials.Common
{
    public record MaterialDto
    (
        Guid Id,
        string Name,
        decimal UnitCost,
        UnitOfMeasure UnitOfMeasure,
        DateTime UpdatedAt
        )
    {
        public static MaterialDto FromDomain(Material material)
        {
            return new MaterialDto(
                material.Id,
                material.Name,
                material.UnitCost,
                material.UnitOfMeasure,
                material.UpdatedAt
            );
        }
    }
}
