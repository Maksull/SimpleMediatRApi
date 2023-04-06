using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SimpleMediatr.Contracts.Controllers.Products;
using SimpleMediatr.MediatR.Commands.Products;
using SimpleMediatr.MediatR.Notifications;
using SimpleMediatr.MediatR.Queries.Products;
using SimpleMediatr.Models;

namespace SimpleMediatR.Endpoints
{
    public static class ProductsEndpoints
    {
        public static IEndpointRouteBuilder MapProductsEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("api/mini/products", GetProducts);
            app.MapGet("api/mini/products/{id:long}", GetProduct);
            app.MapPost("api/mini/products", CreateProduct);
            app.MapPut("api/mini/products", UpdateProduct);
            app.MapDelete("api/mini/products", DeleteProduct);

            return app;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async static Task<IResult> GetProducts(IMediator mediator)
        {
            try
            {
                var products = await mediator.Send(new GetProductsQuery());

                if (products.Any())
                {
                    return Results.Ok(products);
                }

                return Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async static Task<IResult> GetProduct(long id, IMediator mediator)
        {
            try
            {
                var product = await mediator.Send(new GetProductByIdQuery(id));

                if (product != null)
                {
                    return Results.Ok(product);
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
        public async static Task<IResult> CreateProduct(CreateProductDto request, IMediator mediator, IMapper mapper)
        {
            try
            {
                //var product = request.Adapt<Product>();

                var product = mapper.Map<Product>(request);

                var productToReturn = await mediator.Send(new CreateProductCommand(product));

                await mediator.Publish(new ProductAddedNotification(product));

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
        public async static Task<IResult> UpdateProduct(UpdateProductDto request, IMediator mediator, IMapper mapper)
        {
            try
            {
                //var product = request.Adapt<Product>();

                var product = mapper.Map<Product>(request);

                var p = await mediator.Send(new UpdateProductCommand(product));

                if (p != null)
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
        public async static Task<IResult> DeleteProduct(long id, IMediator mediator)
        {
            try
            {
                var product = await mediator.Send(new DeleteProductCommand(id));

                if (product != null)
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
