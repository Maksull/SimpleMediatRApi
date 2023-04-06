using MediatR;
using SimpleMediatr.MediatR.Queries.Categories;
using SimpleMediatr.Models;
using SimpleMediatr.Services.Interfaces;

namespace SimpleMediatr.MediatR.Handlers.Categories
{
    public sealed class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, Category?>
    {
        private readonly ICategoryService _categoryService;

        public GetCategoryByIdHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<Category?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _categoryService.GetCategory(request.Id);
        }
    }
}
