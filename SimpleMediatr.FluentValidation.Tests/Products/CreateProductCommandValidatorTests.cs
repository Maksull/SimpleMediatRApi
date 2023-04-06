using SimpleMediatr.FluentValidation.Products;
using SimpleMediatr.MediatR.Commands.Products;
using SimpleMediatr.Models;

namespace SimpleMediatr.FluentValidation.Tests.Products
{
    public sealed class CreateProductCommandValidatorTests
    {
        [Fact]
        public void Validate_WhenCalled_ShouldNotHaveErrors_For_ValidCreateProductCommand()
        {
            //Arrange
            Product product = new()
            {
                ProductId = 1,
                Name = "1234512345",
                Price = 1,
                CategoryId = 1,
            };
            CreateProductCommand createProductCommand = new(product);
            CreateProductCommandValidator validator = new();

            //Act
            var result = validator.Validate(createProductCommand);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_WhenCalled_ShouldHaveErrors_For_Invalid_CreateProductCommand()
        {
            //Arrange
            Product product = new()
            {
                ProductId = 1,
                Name = "12345",
                Price = 1,
                CategoryId = 1,
            };
            CreateProductCommand createProductCommand = new(product);
            CreateProductCommandValidator validator = new();

            //Act
            var result = validator.Validate(createProductCommand);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Product.Name");
        }
    }
}
