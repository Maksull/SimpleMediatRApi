using SimpleMediatr.MediatR.Commands.Categories;
using SimpleMediatr.MediatR.Commands.Products;
using SimpleMediatr.MediatR.Handlers.Categories;
using SimpleMediatr.MediatR.Handlers.Products;
using SimpleMediatr.Models;
using SimpleMediatr.Services.Interfaces;

namespace SimpleMediatr.MediatR.Tests.Handlers.Categories
{
    public sealed class DeleteCategoryHandlerTests
    {
        private readonly Mock<ICategoryService> _service;
        private readonly DeleteCategoryHandler _handler;

        public DeleteCategoryHandlerTests()
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
            _service.Setup(s => s.DeleteCategory(It.IsAny<long>()))
                .ReturnsAsync(category);

            //Act
            var result = _handler.Handle(new DeleteCategoryCommand(1), CancellationToken.None).Result;

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
            _service.Setup(s => s.DeleteCategory(It.IsAny<long>()))
                .ReturnsAsync((Category)null);

            //Act
            var result = _handler.Handle(new DeleteCategoryCommand(1), CancellationToken.None).Result;

            //Assert
            result.Should().BeNull();
        }
    }
}
