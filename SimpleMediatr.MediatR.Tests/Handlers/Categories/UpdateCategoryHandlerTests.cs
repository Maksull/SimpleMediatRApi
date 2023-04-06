using SimpleMediatr.MediatR.Commands.Categories;
using SimpleMediatr.MediatR.Commands.Products;
using SimpleMediatr.MediatR.Handlers.Categories;
using SimpleMediatr.Models;
using SimpleMediatr.Services.Interfaces;

namespace SimpleMediatr.MediatR.Tests.Handlers.Categories
{
    public sealed class UpdateCategoryHandlerTests
    {
        private readonly Mock<ICategoryService> _service;
        private readonly UpdateCategoryHandler _handler;

        public UpdateCategoryHandlerTests()
        {
            _service = new();
            _handler = new(_service.Object);
        }

        [Fact]
        public void Handle_WhenCalled_ReturnProduct()
        {
            //Arrange
            Category category = new()
            {
                CategoryId = 1,
                Name = "First",
            };
            _service.Setup(s => s.UpdateCategory(It.IsAny<Category>()))
                .ReturnsAsync(category);

            //Act
            var result = _handler.Handle(new UpdateCategoryCommand(category), CancellationToken.None).Result;

            //Assert
            result.Should().BeOfType<Category>();
            result.Should().BeEquivalentTo(category);
        }

        [Fact]
        public void Handle_WhenCalled_ReturnNull()
        {
            //Arrange
            Category category = new()
            {
                CategoryId = 1,
                Name = "First",
            };
            _service.Setup(s => s.UpdateCategory(It.IsAny<Category>()))
                .ReturnsAsync((Category)null);

            //Act
            var result = _handler.Handle(new UpdateCategoryCommand(category), CancellationToken.None).Result;

            //Assert
            result.Should().BeNull();
        }
    }
}
