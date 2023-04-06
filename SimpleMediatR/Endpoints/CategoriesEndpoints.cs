using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleMediatr.Contracts.Controllers.Categories;
using SimpleMediatr.MediatR.Commands.Categories;
using SimpleMediatr.MediatR.Queries.Categories;
using SimpleMediatr.Models;

namespace SimpleMediatR.Endpoints
{
    public static class CategoriesEndpoints
    {
        public static IEndpointRouteBuilder MapCategoriesEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("api/mini/categories", GetCategories);
            app.MapGet("api/mini/categories/{id:long}", GetCategory);
            app.MapPost("api/mini/categories", CreateCategory);
            app.MapPut("api/mini/categories", UpdateCategory);
            app.MapDelete("api/mini/categories", DeleteCategory);

            return app;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Category>))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async static Task<IResult> GetCategories(IMediator mediator)
        {
            try
            {
                var categories = await mediator.Send(new GetCategoriesQuery());

                if (categories.Any())
                {
                    return Results.Ok(categories);
                }

                return Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Category))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async static Task<IResult> GetCategory(long id, IMediator mediator)
        {
            try
            {
                var category = await mediator.Send(new GetCategoryByIdQuery(id));

                if (category != null)
                {
                    return Results.Ok(category);
                }

                return Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async static Task<IResult> CreateCategory(CreateCategoryDto request, IMediator mediator, IMapper mapper)
        {
            try
            {
                //var category = request.Adapt<Category>();

                var category = mapper.From(request).AdaptToType<Category>();

                var c = await mediator.Send(new CreateCategoryCommand(category));

                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResult))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async static Task<IResult> UpdateCategory(UpdateCategoryDto request, IMediator mediator, IMapper mapper)
        {
            try
            {
                //var category = request.Adapt<Category>();

                var category = mapper.From(request).AdaptToType<Category>();

                var c = await mediator.Send(new UpdateCategoryCommand(category));

                if (c != null)
                {
                    return Results.Ok();
                }

                return Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResult))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async static Task<IResult> DeleteCategory(long id, IMediator mediator)
        {
            try
            {
                var category = await mediator.Send(new DeleteCategoryCommand(id));

                if (category != null)
                {
                    return Results.Ok();
                }

                return Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
