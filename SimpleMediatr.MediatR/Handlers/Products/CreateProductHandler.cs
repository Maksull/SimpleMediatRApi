using MediatR;
using SimpleMediatr.MediatR.Commands.Products;
using SimpleMediatr.Models;
using SimpleMediatr.Services.Interfaces;

namespace SimpleMediatr.MediatR.Handlers.Products
{
    public sealed class CreateProductHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly IProductService _productService;

        public CreateProductHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellation)
        {
            var product = await _productService.CreateProduct(request.Product);

            return product;
        }
    }
}
