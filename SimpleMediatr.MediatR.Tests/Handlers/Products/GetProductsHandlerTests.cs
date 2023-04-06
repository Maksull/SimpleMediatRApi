using SimpleMediatr.MediatR.Handlers.Products;
using SimpleMediatr.MediatR.Queries.Products;
using SimpleMediatr.Models;
using SimpleMediatr.Services.Interfaces;

namespace SimpleMediatr.MediatR.Tests.Handlers.Products
{
    public sealed class GetProductsHandlerTests
    {
        private readonly Mock<IProductService> _service;
        private readonly GetProductsHandler _handler;

        public GetProductsHandlerTests()
        {
            _service = new();
            _handler = new(_service.Object);
        }

        [Fact]
        public void Handle_WhenCalled_ReturnProduct()
        {
            //Arrange
            List<Product> products = new()
            {
                new()
                {
                    ProductId = 1,
                    Name = "First",
                    Price = 1,
                    CategoryId = 1,
                },
                new()
                {
                    ProductId = 2,
                    Name = "Second",
                    Price = 2,
                    CategoryId = 2,
                },
            };
            _service.Setup(s => s.GetProducts()).Returns(products);

            //Act
            var result = _handler.Handle(new GetProductsQuery(), CancellationToken.None).Result;

            //Assert
            result.Should().BeOfType<List<Product>>();
            result.Should().BeEquivalentTo(products);
        }

        [Fact]
        public void Handle_WhenCalled_ReturnEmpty()
        {
            //Arrange
            var products = Enumerable.Empty<Product>();
            _service.Setup(s => s.GetProducts()).Returns(products);

            //Act
            var result = _handler.Handle(new GetProductsQuery(), CancellationToken.None).Result;

            //Assert
            result.Should().BeEmpty();
        }
    }
}
