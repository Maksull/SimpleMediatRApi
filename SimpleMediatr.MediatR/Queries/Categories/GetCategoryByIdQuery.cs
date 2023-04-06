using MediatR;
using SimpleMediatr.Models;

namespace SimpleMediatr.MediatR.Queries.Categories
{
    public sealed record GetCategoryByIdQuery(long Id) : IRequest<Category?>;
}
