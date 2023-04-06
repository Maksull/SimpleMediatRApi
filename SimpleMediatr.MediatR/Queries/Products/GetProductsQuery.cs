using MediatR;
using SimpleMediatr.Models;

namespace SimpleMediatr.MediatR.Queries.Products
{
    public sealed record GetProductsQuery() : IRequest<IEnumerable<Product>>;
}
