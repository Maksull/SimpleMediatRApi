using SimpleMediatr.Contracts.Controllers.Products;
using SimpleMediatr.FluentValidation.Products;

namespace SimpleMediatr.FluentValidation.Tests.Products
{
    public sealed class UpdateProductDtoValidatorTests
    {
        [Fact]
        public void Validate_WhenCalled_ShouldNotHaveErrors_For_ValidUpdateProductDto()
        {
            //Arrange
            UpdateProductDto updateProductDto = new()
            {
                ProductId = 1,
                Name = "Test",
                Price = 1,
                CategoryId = 1
            };
            UpdateProductDtoValidator validator = new();

            //Act
            var result = validator.Validate(updateProductDto);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_WhenCalled_ShouldHaveErrors_For_Invalid_UpdateProductDto()
        {
            //Arrange
            UpdateProductDto updateProductDto = new()
            {
                ProductId = 0,
                Name = " ",
                Price = 0,
                CategoryId = 0
            };
            UpdateProductDtoValidator validator = new();

            //Act
            var result = validator.Validate(updateProductDto);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "ProductId");
            result.Errors.Should().Contain(e => e.PropertyName == "Name");
            result.Errors.Should().Contain(e => e.PropertyName == "Price");
            result.Errors.Should().Contain(e => e.PropertyName == "CategoryId");
        }
    }
}
