using CostCalculator.Application.Common.Interfaces;
using CostCalculator.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CostCalculator.Application.Materials.Commands.UpdateMaterial
{
    public record UpdateMaterialCommand(
        Guid Id,
        string Name,
        decimal UnitCost,
        UnitOfMeasure UnitOfMeasure
        ) : IRequest;


    public class UpdateMaterialCommandHandler : IRequestHandler<UpdateMaterialCommand>
    {
            private readonly IApplicationDbContext _context;

            public UpdateMaterialCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task Handle(UpdateMaterialCommand request, CancellationToken cancellationToken)
            {
            var material = await _context.Materials
                .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken) ?? throw new KeyNotFoundException($"Material with ID {request.Id} not found.");
            material.UpdateDetails(
                    request.Name,
                    request.UnitCost,
                    request.UnitOfMeasure
                );

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }

