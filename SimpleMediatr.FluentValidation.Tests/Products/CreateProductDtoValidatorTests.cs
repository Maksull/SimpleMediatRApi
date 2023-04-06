using SimpleMediatr.Contracts.Controllers.Products;
using SimpleMediatr.FluentValidation.Products;

namespace SimpleMediatr.FluentValidation.Tests.Products
{
    public sealed class CreateProductDtoValidatorTests
    {
        [Fact]
        public void Validate_WhenCalled_ShouldNotHaveErrors_For_ValidCreateProductDto()
        {
            //Arrange
            CreateProductDto createProductDto = new()
            {
                Name = "Test",
                Price = 1,
                CategoryId = 1,
            };
            CreateProductDtoValidator validator = new();

            //Act
            var result = validator.Validate(createProductDto);

            //Assert
            result.IsValid.Should().BeTrue();
        }


        [Fact]
        public void Validate_WhenCalled_ShouldHaveErrors_For_InvalidCreateProductDto()
        {
            //Arrange
            CreateProductDto createProductDto = new()
            {
                Name = "   ",
                Price = 0,
                CategoryId = 0,
            };
            CreateProductDtoValidator validator = new();

            //Act
            var result = validator.Validate(createProductDto);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(3);
            result.Errors.Should().Contain(e => e.PropertyName == "Name");
            result.Errors.Should().Contain(e => e.PropertyName == "Price");
            result.Errors.Should().Contain(e => e.PropertyName == "CategoryId");
        }
    }
}