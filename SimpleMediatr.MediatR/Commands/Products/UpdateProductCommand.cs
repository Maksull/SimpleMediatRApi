using MediatR;
using SimpleMediatr.Models;

namespace SimpleMediatr.MediatR.Commands.Products
{
    public sealed record UpdateProductCommand(Product Product) : IRequest<Product>;
}
