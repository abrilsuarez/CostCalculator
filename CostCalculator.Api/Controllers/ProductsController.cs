using CostCalculator.Application.Materials.Commands.UpdateMaterial;
using CostCalculator.Application.Materials.Common;
using CostCalculator.Application.Materials.Queries.GetMaterialById;
using CostCalculator.Application.Products.Commands.CreateProduct;
using CostCalculator.Application.Products.Commands.DeleteProduct;
using CostCalculator.Application.Products.Commands.UpdateProduct;
using CostCalculator.Application.Products.Common;
using CostCalculator.Application.Products.Queries.GetProductById;
using CostCalculator.Application.Products.Queries.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CostCalculator.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ISender _sender;

        public ProductsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetProductsQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<List<MaterialDto>>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetProductByIdQuery(id), cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(
            [FromBody] CreateProductCommand command,
            CancellationToken cancellationToken)
        {
            var productId = await _sender.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetAll), new { id = productId }, productId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(
            Guid id,
            [FromBody] UpdateProductCommand command,
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
            await _sender.Send(new DeleteProductCommand(id), cancellationToken);
            return NoContent();
        }

    }
}
