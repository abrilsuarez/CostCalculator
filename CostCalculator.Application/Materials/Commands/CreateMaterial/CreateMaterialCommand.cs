using CostCalculator.Application.Common.Interfaces;
using CostCalculator.Domain.Entities;
using CostCalculator.Domain.Enums;
using MediatR;

namespace CostCalculator.Application.Materials.Commands.CreateMaterial
{
    public record CreateMaterialCommand(
        string Name,
        decimal UnitCost,
        UnitOfMeasure UnitOfMeasure
    ) : IRequest<Guid>;

    public class CreateMaterialCommandHandler : IRequestHandler<CreateMaterialCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateMaterialCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateMaterialCommand request, CancellationToken cancellationToken)
        {
            var material = new Material(
                request.Name,
                request.UnitCost,
                request.UnitOfMeasure
            );

            _context.Materials.Add(material);
            await _context.SaveChangesAsync(cancellationToken);

            return material.Id;
        }
    }
}
