using CostCalculator.Application.Common.Interfaces;
using CostCalculator.Application.Products.Commands.CreateProduct;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CostCalculator.Application.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand(
        Guid Id,
        string Name,
        string Description,
        decimal LaborCost,
        decimal ProfitMarginPercentage,
        List<RecipeItemInput> RecipeItems
    ) : IRequest;

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .Include(p => p.RecipeItems)
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken)
                ?? throw new KeyNotFoundException($"Product with ID {request.Id} not found.");

            product.UpdateDetails(
                request.Name,
                request.Description,
                request.LaborCost,
                request.ProfitMarginPercentage
            );

            var recipeInputTuples = request.RecipeItems
            .Select(i => (i.MaterialId, i.QuantityRequired))
            .ToList(); 
            product.UpdateRecipe(recipeInputTuples);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
