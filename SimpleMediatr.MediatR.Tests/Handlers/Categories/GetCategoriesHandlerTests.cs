using SimpleMediatr.MediatR.Handlers.Categories;
using SimpleMediatr.MediatR.Handlers.Products;
using SimpleMediatr.MediatR.Queries.Categories;
using SimpleMediatr.MediatR.Queries.Products;
using SimpleMediatr.Models;
using SimpleMediatr.Services.Interfaces;

namespace SimpleMediatr.MediatR.Tests.Handlers.Categories
{
    public sealed class GetCategoriesHandlerTests
    {
        private readonly Mock<ICategoryService> _service;
        private readonly GetCategoriesHandler _handler;

        public GetCategoriesHandlerTests()
        {
            _service = new();
            _handler = new(_service.Object);
        }

        [Fact]
        public void Handle_WhenCalled_ReturnProduct()
        {
            //Arrange
            List<Category> categories = new()
            {
                new()
                {
                    CategoryId = 1,
                    Name = "First",
                },
                new()
                {
                    CategoryId = 2,
                    Name = "Second",
                },
            };
            _service.Setup(s => s.GetCategories()).Returns(categories);

            //Act
            var result = _handler.Handle(new GetCategoriesQuery(), CancellationToken.None).Result;

            //Assert
            result.Should().BeOfType<List<Category>>();
            result.Should().BeEquivalentTo(categories);
        }

        [Fact]
        public void Handle_WhenCalled_ReturnEmpty()
        {
            //Arrange
            var products = Enumerable.Empty<Category>();
            _service.Setup(s => s.GetCategories()).Returns(products);

            //Act
            var result = _handler.Handle(new GetCategoriesQuery(), CancellationToken.None).Result;

            //Assert
            result.Should().BeEmpty();
        }
    }
}
