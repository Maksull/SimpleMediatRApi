using FluentValidation;
using SimpleMediatr.Contracts.Controllers.Categories;

namespace SimpleMediatr.FluentValidation.Categories
{
    public sealed class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
        }
    }
}
