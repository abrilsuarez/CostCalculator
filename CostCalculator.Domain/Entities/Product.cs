using System;
using System.Collections.Generic;
using System.Text;

namespace CostCalculator.Domain.Entities
{
    public class Product
    {
        private readonly List<ProductRecipeItem> _recipeItems = new();

        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal LaborCost { get; private set; }
        public decimal ProfitMarginPercentage { get; private set; }
        public IReadOnlyCollection<ProductRecipeItem> RecipeItems => _recipeItems.AsReadOnly();

        private Product() { }

        public Product(string name, string description, decimal laborCost, decimal profitMarginPercentage)
        {
            Id = Guid.NewGuid();
            UpdateDetails(name, description, laborCost, profitMarginPercentage);
        }

        public void UpdateDetails(string name, string description, decimal laborCost, decimal profitMarginPercentage)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be empty.", nameof(name));

            if (laborCost < 0)
                throw new ArgumentOutOfRangeException(nameof(laborCost), "Labor cost cannot be negative.");

            if (profitMarginPercentage < 0)
                throw new ArgumentOutOfRangeException(nameof(profitMarginPercentage), "Profit margin percentage cannot be negative.");

            Name = name.Trim();
            Description = description?.Trim() ?? string.Empty;
            LaborCost = laborCost;
            ProfitMarginPercentage = profitMarginPercentage;
        }

        public void UpdateRecipe(List<(Guid MaterialId, decimal QuantityRequired)> newItems)
        {
            var materialIdsToKeep = newItems.Select(i => i.MaterialId).ToList();

            var itemsToRemove = _recipeItems
                .Where(i => !materialIdsToKeep.Contains(i.MaterialId))
                .ToList();

            foreach (var item in itemsToRemove)
                RemoveRecipeItem(item.MaterialId);
            foreach (var item in newItems)
                AddOrUpdateRecipeItem(item.MaterialId, item.QuantityRequired);
        }

        public void AddOrUpdateRecipeItem(Guid materialId, decimal quantity)
        {
            var existingItem = _recipeItems.FirstOrDefault(i => i.MaterialId == materialId);
            if (existingItem is not null)
            {
                existingItem.UpdateQuantity(quantity);
            }
            else
            {
                _recipeItems.Add(new ProductRecipeItem(Id, materialId, quantity));
            }
        }

        public void RemoveRecipeItem(Guid materialId)
        {
            var item = _recipeItems.FirstOrDefault(i => i.MaterialId == materialId);
            if (item is not null)
            {
                _recipeItems.Remove(item);
            }
        }

        public decimal CalculateMaterialsCost() => _recipeItems.Sum(item => item.Cost());

        public decimal CalculateTotalCost() => CalculateMaterialsCost() + LaborCost;

        public decimal CalculateSuggestedPrice()
        {
            var totalCost = CalculateTotalCost();
            return totalCost * (1 + (ProfitMarginPercentage / 100m));
        }
    }
}
