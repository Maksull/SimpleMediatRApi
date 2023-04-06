using MediatR;
using SimpleMediatr.MediatR.Queries.Products;
using SimpleMediatr.Models;
using SimpleMediatr.Services.Interfaces;

namespace SimpleMediatr.MediatR.Handlers.Products
{
    public sealed class GetProductsHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly IProductService _productService;

        public GetProductsHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return _productService.GetProducts();
        }
    }
}
