
using CostCalculator.Application.Materials.Commands.CreateMaterial;
using CostCalculator.Application.Materials.Commands.DeleteMaterial;
using CostCalculator.Application.Materials.Commands.UpdateMaterial;
using CostCalculator.Application.Materials.Common;
using CostCalculator.Application.Materials.Queries.GetMaterials;
using CostCalculator.Application.Materials.Queries.GetMaterialById;
using CostCalculator.Application.Products.Commands.DeleteProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CostCalculator.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialsController : ControllerBase
    {
        private readonly ISender _sender;

        public MaterialsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<ActionResult<List<MaterialDto>>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetMaterialsQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<List<MaterialDto>>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetMaterialByIdQuery(id), cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(
            [FromBody] CreateMaterialCommand command,
            CancellationToken cancellationToken)
        {
            var materialId = await _sender.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetAll), new { id = materialId }, materialId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(
            Guid id,
            [FromBody] UpdateMaterialCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest("The ID in the URL does not match the ID in the request body.");
            }

            await _sender.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult<Guid>> Delete(
            Guid id,
            CancellationToken cancellationToken)
        {
            await _sender.Send(new DeleteMaterialCommand(id), cancellationToken);
            return NoContent();
        }
    }
}   
