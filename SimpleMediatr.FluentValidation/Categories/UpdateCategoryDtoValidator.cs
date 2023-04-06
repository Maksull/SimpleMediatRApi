using FluentValidation;
using SimpleMediatr.Contracts.Controllers.Categories;

namespace SimpleMediatr.FluentValidation.Categories
{
    public sealed class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryDtoValidator()
        {
            RuleFor(c => c.CategoryId).GreaterThan(0);
            RuleFor(c => c.Name).NotEmpty();
        }
    }
}
