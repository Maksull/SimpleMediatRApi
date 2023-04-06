using SimpleMediatr.Contracts.Controllers.Categories;
using SimpleMediatr.FluentValidation.Categories;

namespace SimpleMediatr.FluentValidation.Tests.Category
{
    public sealed class CreateCategoryDtoValidatorTests
    {
        [Fact]
        public void Validate_WhenCalled_ShouldNotHaveErrors_For_ValidCreateCategoryDto()
        {
            //Arrange
            CreateCategoryDto createCategoryDto = new()
            {
                Name = "Test",
            };
            CreateCategoryDtoValidator validator = new();

            //Act
            var result = validator.Validate(createCategoryDto);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_WhenCalled_ShouldHaveErrors_For_InvalidCreateCategoryDto()
        {
            //Arrange
            CreateCategoryDto createCategoryDto = new()
            {
                Name = " ",
            };
            CreateCategoryDtoValidator validator = new();

            //Act
            var result = validator.Validate(createCategoryDto);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Name");
        }
    }
}
