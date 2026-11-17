using CostCalculator.Application.Common.Interfaces;
using CostCalculator.Application.Materials.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CostCalculator.Application.Materials.Queries.GetMaterials
{
    public record GetMaterialsQuery : IRequest<List<MaterialDto>>;

    public class GetMaterialsQueryHandler(IApplicationDbContext context) : IRequestHandler<GetMaterialsQuery, List<MaterialDto>>
    {
        private readonly IApplicationDbContext _context = context;

        public async Task<List<MaterialDto>> Handle(GetMaterialsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Materials
                .AsNoTracking()
                .Select(m => MaterialDto.MapFrom(m))
                .ToListAsync(cancellationToken);
        }
    }
}
