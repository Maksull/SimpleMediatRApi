using MediatR;
using SimpleMediatr.MediatR.Queries.Categories;
using SimpleMediatr.Models;
using SimpleMediatr.Services.Interfaces;

namespace SimpleMediatr.MediatR.Handlers.Categories
{
    public sealed class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<Category>>
    {
        private readonly ICategoryService _categoryService;

        public GetCategoriesHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IEnumerable<Category>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            return _categoryService.GetCategories();
        }
    }
}
