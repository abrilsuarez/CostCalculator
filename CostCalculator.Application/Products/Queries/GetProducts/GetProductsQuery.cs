using CostCalculator.Application.Common.Interfaces;
using CostCalculator.Application.Products.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CostCalculator.Application.Products.Queries.GetProducts
{
    public record GetProductsQuery : IRequest<List<ProductDto>>;

    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetProductsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.Products
                .AsNoTracking()
                .Include(p => p.RecipeItems)
                    .ThenInclude(ri => ri.Material)
                .ToListAsync(cancellationToken);

            return products.Select(p => ProductDto.MapFrom(p)).ToList();
        }
    }
}
