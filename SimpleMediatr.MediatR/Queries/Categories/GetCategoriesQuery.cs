using MediatR;
using SimpleMediatr.Models;

namespace SimpleMediatr.MediatR.Queries.Categories
{
    public sealed record GetCategoriesQuery() : IRequest<IEnumerable<Category>?>;
}
