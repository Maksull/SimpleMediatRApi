using MediatR;
using SimpleMediatr.Models;

namespace SimpleMediatr.MediatR.Commands.Categories
{
    public sealed record CreateCategoryCommand(Category Category) : IRequest<Category>;
}
