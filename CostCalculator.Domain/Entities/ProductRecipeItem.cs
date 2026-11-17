using System;
using System.Collections.Generic;
using System.Text;

namespace CostCalculator.Domain.Entities
{
    public class ProductRecipeItem
    {
        public Guid ProductId { get; private set; }
        public Product Product { get; private set; } = null!;

        public Guid MaterialId { get; private set; }
        public Material Material { get; private set; } = null!;

        public decimal QuantityRequired { get; private set; }

        private ProductRecipeItem() { }

        public ProductRecipeItem(Guid productId, Guid materialId, decimal quantityRequired)
        {
            if (quantityRequired <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantityRequired), "Quantity required must be greater than zero.");

            ProductId = productId;
            MaterialId = materialId;
            QuantityRequired = quantityRequired;
        }

        public void UpdateQuantity(decimal quantityRequired)
        {
            if (quantityRequired <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantityRequired), "Quantity required must be greater than zero.");

            QuantityRequired = quantityRequired;
        }

        public decimal Cost() => QuantityRequired * Material.UnitCost;
    }
}
