using CostCalculator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CostCalculator.Infrastructure.Persistence.Configurations
{
    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.ToTable("Materials");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(m => m.UnitCost)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(m => m.UnitOfMeasure)
                .IsRequired();

            builder.Property(m => m.UpdatedAt)
                .IsRequired();
        }
    }
}
