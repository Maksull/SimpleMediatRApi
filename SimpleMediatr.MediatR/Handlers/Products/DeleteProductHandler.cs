using MediatR;
using SimpleMediatr.MediatR.Commands.Products;
using SimpleMediatr.Models;
using SimpleMediatr.Services.Interfaces;

namespace SimpleMediatr.MediatR.Handlers.Products
{
    public sealed class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Product?>
    {
        private readonly IProductService _productService;

        public DeleteProductHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<Product?> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            return await _productService.DeleteProduct(request.Id);
        }
    }
}
