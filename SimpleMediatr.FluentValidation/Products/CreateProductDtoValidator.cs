using FluentValidation;
using SimpleMediatr.Contracts.Controllers.Products;

namespace SimpleMediatr.FluentValidation.Products
{
    public sealed class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Price).GreaterThan(0);
            RuleFor(p => p.CategoryId).GreaterThan(0);
        }
    }
}
