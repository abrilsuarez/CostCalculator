using CostCalculator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CostCalculator.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Material> Materials { get; }
        DbSet<Product> Products { get; }
        DbSet<ProductRecipeItem> ProductRecipeItems { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
