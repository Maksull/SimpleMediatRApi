using MediatR;
using SimpleMediatr.MediatR.Commands.Categories;
using SimpleMediatr.Models;
using SimpleMediatr.Services.Interfaces;

namespace SimpleMediatr.MediatR.Handlers.Categories
{
    public sealed class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Category?>
    {
        private readonly ICategoryService _categoryService;

        public UpdateCategoryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<Category?> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _categoryService.UpdateCategory(request.Category);
        }
    }
}
