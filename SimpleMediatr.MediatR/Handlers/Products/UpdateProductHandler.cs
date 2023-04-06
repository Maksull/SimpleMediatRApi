using MediatR;
using SimpleMediatr.MediatR.Commands.Products;
using SimpleMediatr.Models;
using SimpleMediatr.Services.Interfaces;

namespace SimpleMediatr.MediatR.Handlers.Products
{
    public sealed class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Product?>
    {
        private readonly IProductService _productService;

        public UpdateProductHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<Product?> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            return await _productService.UpdateProduct(request.Product);
        }
    }
}
