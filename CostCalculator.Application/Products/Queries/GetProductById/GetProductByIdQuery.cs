using CostCalculator.Application.Common.Interfaces;
using CostCalculator.Application.Products.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CostCalculator.Application.Products.Queries.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto>;
    public class GetProductByIdQueryHandler(IApplicationDbContext context) : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IApplicationDbContext _context = context;
        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .AsNoTracking()
                .Include(p => p.RecipeItems)
                    .ThenInclude(ri => ri.Material)
                .FirstOrDefaultAsync(p => p.Id == request.Id)
                ?? throw new KeyNotFoundException($"Product with ID {request.Id} not found.");

            return ProductDto.FromDomain(product);
        }
    }
}
