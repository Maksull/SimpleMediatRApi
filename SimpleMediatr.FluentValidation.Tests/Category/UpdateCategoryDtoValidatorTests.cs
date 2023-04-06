using SimpleMediatr.Contracts.Controllers.Categories;
using SimpleMediatr.FluentValidation.Categories;

namespace SimpleMediatr.FluentValidation.Tests.Category
{
    public sealed class UpdateCategoryDtoValidatorTests
    {
        [Fact]
        public void Validate_WhenCalled_ShouldNotHaveErrors_For_ValidUpdateCategoryDto()
        {
            //Arrange
            UpdateCategoryDto updateCategoryDto = new()
            {
                CategoryId = 1,
                Name = "Test",
            };
            UpdateCategoryDtoValidator validator = new();

            //Act
            var result = validator.Validate(updateCategoryDto);

            //Assert
            result.IsValid.Should().BeTrue();
        }


        [Fact]
        public void Validate_WhenCalled_ShouldHaveErrors_For_InvalidUpdateCategoryDto()
        {
            //Arrange
            UpdateCategoryDto updateCategoryDto = new()
            {
                CategoryId = 0,
                Name = " ",
            };
            UpdateCategoryDtoValidator validator = new();

            //Act
            var result = validator.Validate(updateCategoryDto);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "CategoryId");
            result.Errors.Should().Contain(e => e.PropertyName == "Name");
        }
    }
}
