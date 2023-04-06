using FluentValidation;
using SimpleMediatr.MediatR.Commands.Products;

namespace SimpleMediatr.FluentValidation.Products
{
    public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(k => k.Product.Name).MinimumLength(10);
        }
    }
}
