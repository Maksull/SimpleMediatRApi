using MediatR;
using SimpleMediatr.Models;

namespace SimpleMediatr.MediatR.Queries.Products
{
    public sealed record GetProductByIdQuery(long Id) : IRequest<Product>;
}
