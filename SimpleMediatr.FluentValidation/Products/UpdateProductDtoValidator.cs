using FluentValidation;
using SimpleMediatr.Contracts.Controllers.Products;

namespace SimpleMediatr.FluentValidation.Products
{
    public sealed class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator()
        {
            RuleFor(p => p.ProductId).GreaterThan(0);
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Price).GreaterThan(0);
            RuleFor(p => p.CategoryId).GreaterThan(0);
        }
    }
}
