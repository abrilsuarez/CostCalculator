using CostCalculator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CostCalculator.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Description)
                .HasMaxLength(1000);

            builder.Property(p => p.LaborCost)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(p => p.ProfitMarginPercentage)
                .HasPrecision(5, 2)
                .IsRequired();

            builder.Metadata
                .FindNavigation(nameof(Product.RecipeItems))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
