using SimpleMediatr.MediatR.Commands.Products;
using SimpleMediatr.MediatR.Handlers.Products;
using SimpleMediatr.Models;
using SimpleMediatr.Services.Interfaces;

namespace SimpleMediatr.MediatR.Tests.Handlers.Products
{
    public sealed class UpdateProductHandlerTests
    {
        private readonly Mock<IProductService> _service;
        private readonly UpdateProductHandler _handler;

        public UpdateProductHandlerTests()
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
            _service.Setup(s => s.UpdateProduct(It.IsAny<Product>()))
                .ReturnsAsync(product);

            //Act
            var result = _handler.Handle(new UpdateProductCommand(product), CancellationToken.None).Result;

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
            _service.Setup(s => s.UpdateProduct(It.IsAny<Product>()))
                .ReturnsAsync((Product)null);

            //Act
            var result = _handler.Handle(new UpdateProductCommand(product), CancellationToken.None).Result;

            //Assert
            result.Should().BeNull();
        }
    }
}
