using CostCalculator.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CostCalculator.Domain.Entities
{
    public class Material
    {
        public Guid Id { get; set; }
        public string Name { get; private set; } = string.Empty;
        public decimal UnitCost { get; private set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public DateTime UpdatedAt { get; set; }


        private Material() { }

        public Material(string name, decimal unitCost, UnitOfMeasure unitOfMeasure)
        {
            Id = Guid.NewGuid();
            UpdateDetails(name, unitCost, unitOfMeasure);
        }

        public void UpdateDetails(string name, decimal unitCost, UnitOfMeasure unitOfMeasure)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));

            if (unitCost < 0)
                throw new ArgumentOutOfRangeException(nameof(unitCost), "Unit cost cannot be negative.");

            Name = name.Trim();
            UnitCost = unitCost;
            UnitOfMeasure = unitOfMeasure;
            UpdatedAt = DateTime.UtcNow;
        }
    }

}