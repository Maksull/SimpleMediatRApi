using MediatR;
using SimpleMediatr.Models;

namespace SimpleMediatr.MediatR.Commands.Categories
{
    public sealed record DeleteCategoryCommand(long Id) : IRequest<Category>;
}
