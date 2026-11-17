using CostCalculator.Application.Common.Interfaces;
using CostCalculator.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CostCalculator.Application.Materials.Commands.DeleteMaterial
{
    public record DeleteMaterialCommand(Guid Id) : IRequest;

    public class DeleteMaterialCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteMaterialCommand>
    {
        private readonly IApplicationDbContext _context = context;

        public async Task Handle(DeleteMaterialCommand request, CancellationToken cancellationToken)
        {
            var material = await _context.Materials
                .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken)
                ?? throw new KeyNotFoundException($"Material with ID {request.Id} not found.");

            _context.Materials.Remove(material);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
