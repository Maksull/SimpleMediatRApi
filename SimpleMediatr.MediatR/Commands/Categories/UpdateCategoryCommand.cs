using MediatR;
using SimpleMediatr.Models;

namespace SimpleMediatr.MediatR.Commands.Categories
{
    public sealed record UpdateCategoryCommand(Category Category) : IRequest<Category>;
}
