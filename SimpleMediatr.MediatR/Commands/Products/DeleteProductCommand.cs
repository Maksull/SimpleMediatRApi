using MediatR;
using SimpleMediatr.Models;

namespace SimpleMediatr.MediatR.Commands.Products
{
    public sealed record DeleteProductCommand(long Id) : IRequest<Product>;
}
