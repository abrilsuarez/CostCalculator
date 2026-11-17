using CostCalculator.Application.Common.Interfaces;
using CostCalculator.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CostCalculator.Application.Products.Commands.CreateProduct
{
    public record RecipeItemInput(Guid MaterialId, decimal QuantityRequired);

    public record CreateProductCommand(
        string Name,
        string Description,
        decimal LaborCost,
        decimal ProfitMarginPercentage,
        List<RecipeItemInput> RecipeItems
    ) : IRequest<Guid>;

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var requestedMaterialIds = request.RecipeItems
                .Select(i => i.MaterialId)
                .Distinct()
                .ToList();

            var existingMaterialsCount = await _context.Materials
                .CountAsync(m => requestedMaterialIds.Contains(m.Id), cancellationToken);

            if (existingMaterialsCount != requestedMaterialIds.Count)
            {
                throw new KeyNotFoundException("One or more materials specified in the recipe do not exist.");
            }

            var product = new Product(
                request.Name,
                request.Description,
                request.LaborCost,
                request.ProfitMarginPercentage
            );

            foreach (var item in request.RecipeItems)
            {
                product.AddOrUpdateRecipeItem(item.MaterialId, item.QuantityRequired);
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
