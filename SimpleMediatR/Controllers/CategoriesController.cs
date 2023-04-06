using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleMediatr.Contracts.Controllers.Categories;
using SimpleMediatr.MediatR.Commands.Categories;
using SimpleMediatr.MediatR.Queries.Categories;
using SimpleMediatr.Models;

namespace SimpleMediatR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CategoriesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Category>))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await _mediator.Send(new GetCategoriesQuery());

                if (categories.Any())
                {
                    return Ok(categories);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Category))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetCategory(long id)
        {
            try
            {
                var category = await _mediator.Send(new GetCategoryByIdQuery(id));

                if (category != null)
                {
                    return Ok(category);
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
        public async Task<IActionResult> CreateCategory(CreateCategoryDto request)
        {
            try
            {
                //var category = request.Adapt<Category>();

                var category = _mapper.From(request).AdaptToType<Category>();

                var c = await _mediator.Send(new CreateCategoryCommand(category));

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
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto request)
        {
            try
            {
                //var category = request.Adapt<Category>();

                var category = _mapper.From(request).AdaptToType<Category>();

                var c = await _mediator.Send(new UpdateCategoryCommand(category));

                if (c != null)
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
        public async Task<IActionResult> DeleteCategory(long id)
        {
            try
            {
                var category = await _mediator.Send(new DeleteCategoryCommand(id));

                if (category != null)
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
