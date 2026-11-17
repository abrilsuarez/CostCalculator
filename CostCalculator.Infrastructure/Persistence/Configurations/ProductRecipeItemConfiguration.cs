using CostCalculator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CostCalculator.Infrastructure.Persistence.Configurations
{
    public class ProductRecipeItemConfiguration : IEntityTypeConfiguration<ProductRecipeItem>
    {
        public void Configure(EntityTypeBuilder<ProductRecipeItem> builder)
        {
            builder.ToTable("ProductRecipeItems");

            builder.HasKey(ri => new { ri.ProductId, ri.MaterialId });

            builder.Property(ri => ri.QuantityRequired)
                .HasPrecision(18, 4)
                .IsRequired();

            builder.HasOne(ri => ri.Product)
                .WithMany(p => p.RecipeItems)
                .HasForeignKey(ri => ri.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ri => ri.Material)
                .WithMany()
                .HasForeignKey(ri => ri.MaterialId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
