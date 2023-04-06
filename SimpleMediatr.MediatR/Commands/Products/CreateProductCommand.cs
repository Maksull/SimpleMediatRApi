using MediatR;
using SimpleMediatr.Models;

namespace SimpleMediatr.MediatR.Commands.Products
{
    public sealed record CreateProductCommand(Product Product) : IRequest<Product>;
}
