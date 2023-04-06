using SimpleMediatr.MediatR.Handlers.Products;
using SimpleMediatr.MediatR.Queries.Products;
using SimpleMediatr.Models;
using SimpleMediatr.Services.Interfaces;

namespace SimpleMediatr.MediatR.Tests.Handlers.Products
{
    public sealed class GetProductByIdHandlerTests
    {
        private readonly Mock<IProductService> _service;
        private readonly GetProductByIdHandler _handler;

        public GetProductByIdHandlerTests()
        {
            _service = new();
            _handler = new(_service.Object);
        }

        [Fact]
        public void Handle_WhenCalled_ReturnProduct()
        {
            //Arrange
            Product product = new()
            {
                ProductId = 1,
                Name = "First",
                Price = 1,
                CategoryId = 1,
            };
            _service.Setup(s => s.GetProduct(It.IsAny<long>()))
                .ReturnsAsync(product);

            //Act
            var result = _handler.Handle(new GetProductByIdQuery(1), CancellationToken.None).Result;

            //Assert
            result.Should().BeOfType<Product>();
            result.Should().BeEquivalentTo(product);
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
            _service.Setup(s => s.GetProduct(It.IsAny<long>()))
                .ReturnsAsync((Product)null);

            //Act
            var result = _handler.Handle(new GetProductByIdQuery(1), CancellationToken.None).Result;

            //Assert
            result.Should().BeNull();
        }
    }
}
