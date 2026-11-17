using CostCalculator.Application.Common.Interfaces;
using CostCalculator.Application.Materials.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CostCalculator.Application.Materials.Queries.GetMaterialById
{

    public record GetMaterialByIdQuery(Guid Id) : IRequest<MaterialDto>;
    public class GetMaterialByIdQueryHandler(IApplicationDbContext context) : IRequestHandler<GetMaterialByIdQuery, MaterialDto>
    {
        private readonly IApplicationDbContext _context = context;
        public async Task<MaterialDto> Handle(GetMaterialByIdQuery request, CancellationToken cancellationToken)
        {
            var material = await _context.Materials.AsNoTracking().FirstOrDefaultAsync(m => m.Id == request.Id)
                ?? throw new KeyNotFoundException($"Material with ID {request.Id} not found");

            return MaterialDto.FromDomain(material);
        }
    }
}
