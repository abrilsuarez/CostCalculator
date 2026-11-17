using CostCalculator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CostCalculator.Application.Products.Common
{
    public record ProductDto(
        Guid Id,
        string Name,
        string Description,
        decimal LaborCost,
        decimal ProfitMarginPercentage,
        decimal MaterialsCost,
        decimal TotalCost,
        decimal SuggestedPrice,
        List<ProductRecipeItemDto> RecipeItems
    )
    {
        public static ProductDto MapFrom(Product product)
        {
            return new ProductDto(
                        product.Id,
                        product.Name,
                        product.Description,
                        product.LaborCost,
                        product.ProfitMarginPercentage,
                        product.CalculateMaterialsCost(),
                        product.CalculateTotalCost(),
                        product.CalculateSuggestedPrice(),
                        product.RecipeItems.Select(ri => new ProductRecipeItemDto(
                            ri.MaterialId,
                            ri.Material.Name,
                            ri.Material.UnitOfMeasure,
                            ri.Material.UnitCost,
                            ri.QuantityRequired,
                            ri.Cost()
                        )).ToList()
                    );
        }
    }
}
