using MediatR;
using SimpleMediatr.MediatR.Commands.Categories;
using SimpleMediatr.Models;
using SimpleMediatr.Services.Interfaces;

namespace SimpleMediatr.MediatR.Handlers.Categories
{
    public sealed class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Category?>
    {
        private readonly ICategoryService _categoryService;

        public DeleteCategoryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<Category?> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _categoryService.DeleteCategory(request.Id);
        }
    }
}
