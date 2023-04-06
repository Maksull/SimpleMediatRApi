using SimpleMediatr.MediatR.Handlers.Categories;
using SimpleMediatr.MediatR.Handlers.Products;
using SimpleMediatr.MediatR.Queries.Categories;
using SimpleMediatr.MediatR.Queries.Products;
using SimpleMediatr.Models;
using SimpleMediatr.Services.Interfaces;

namespace SimpleMediatr.MediatR.Tests.Handlers.Categories
{
    public sealed class GetCategoryByIdHandlerTests
    {
        private readonly Mock<ICategoryService> _service;
        private readonly GetCategoryByIdHandler _handler;

        public GetCategoryByIdHandlerTests()
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
            _service.Setup(s => s.GetCategory(It.IsAny<long>()))
                .ReturnsAsync(category);

            //Act
            var result = _handler.Handle(new GetCategoryByIdQuery(1), CancellationToken.None).Result;

            //Assert
            result.Should().BeOfType<Category>();
            result.Should().BeEquivalentTo(category);
        }

        [Fact]
        public void Handle_WhenCalled_ReturnNull()
        {
            //Arrange
            Product product = new()
            {
                ProductId = 1,
                Name = "First",
                Price = 1,
                CategoryId = 1,
            };
            _service.Setup(s => s.GetCategory(It.IsAny<long>()))
                .ReturnsAsync((Category)null);

            //Act
            var result = _handler.Handle(new GetCategoryByIdQuery(1), CancellationToken.None).Result;

            //Assert
            result.Should().BeNull();
        }
    }
}
