using SimpleMediatr.MediatR.Commands.Products;
using SimpleMediatr.MediatR.Handlers.Products;
using SimpleMediatr.Models;
using SimpleMediatr.Services.Interfaces;

namespace SimpleMediatr.MediatR.Tests.Handlers.Products
{
    public sealed class DeleteProductHandlerTests
    {
        private readonly Mock<IProductService> _service;
        private readonly DeleteProductHandler _handler;

        public DeleteProductHandlerTests()
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
            _service.Setup(s => s.DeleteProduct(It.IsAny<long>()))
                .ReturnsAsync(product);

            //Act
            var result = _handler.Handle(new DeleteProductCommand(1), CancellationToken.None).Result;

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
            _service.Setup(s => s.DeleteProduct(It.IsAny<long>()))
                .ReturnsAsync((Product)null);

            //Act
            var result = _handler.Handle(new DeleteProductCommand(1), CancellationToken.None).Result;

            //Assert
            result.Should().BeNull();
        }
    }
}
