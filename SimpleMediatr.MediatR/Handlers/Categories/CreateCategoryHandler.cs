using MediatR;
using SimpleMediatr.MediatR.Commands.Categories;
using SimpleMediatr.Models;
using SimpleMediatr.Services.Interfaces;

namespace SimpleMediatr.MediatR.Handlers.Categories
{
    public sealed class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Category>
    {
        private readonly ICategoryService _categoryService;

        public CreateCategoryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _categoryService.CreateCategory(request.Category);
        }
    }
}
