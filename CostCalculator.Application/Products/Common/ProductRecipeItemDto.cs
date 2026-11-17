using CostCalculator.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CostCalculator.Application.Products.Common
{
    public record ProductRecipeItemDto
    (
        Guid MaterialId,
        string MaterialName,
        UnitOfMeasure UnitOfMeasure,
        decimal UnitCost,
        decimal QuantityRequired,
        decimal SubtotalCost
    );
}
