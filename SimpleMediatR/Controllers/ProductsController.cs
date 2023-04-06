using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SimpleMediatr.Contracts.Controllers.Products;
using SimpleMediatr.MediatR.Commands.Products;
using SimpleMediatr.MediatR.Notifications;
using SimpleMediatr.MediatR.Queries.Products;
using SimpleMediatr.Models;

namespace SimpleMediatR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await _mediator.Send(new GetProductsQuery());

                if (products.Any())
                {
                    return Ok(products);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetProduct(long id)
        {
            try
            {
                var product = await _mediator.Send(new GetProductByIdQuery(id));

                if (product != null)
                {
                    return Ok(product);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult> CreateProduct(CreateProductDto request)
        {
            try
            {
                //var product = request.Adapt<Product>();

                var product = _mapper.Map<Product>(request);

                var productToReturn = await _mediator.Send(new CreateProductCommand(product));

                await _mediator.Publish(new ProductAddedNotification(product));

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResult))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto request)
        {
            try
            {
                //var product = request.Adapt<Product>();

                var product = _mapper.Map<Product>(request);

                var p = await _mediator.Send(new UpdateProductCommand(product));

                if (p != null)
                {
                    return Ok();
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResult))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            try
            {
                var product = await _mediator.Send(new DeleteProductCommand(id));

                if (product != null)
                {
                    return Ok();
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
