using Microsoft.Extensions.Logging;
using SimpleMediatr.MediatR.Behaviors;
using SimpleMediatr.MediatR.Commands.Products;
using SimpleMediatr.Models;

namespace SimpleMediatr.MediatR.Tests.Behaviors
{
    public sealed class LoggingBehaviorTests
    {
        [Fact]
        public async Task Handle_LogsRequestAndResponse()
        {
            // Arrange
            Product product = new()
            {
                ProductId = 1,
                Name = "First",
                Price = 1,
                CategoryId = 1,
            };

            Mock<ILogger<LoggingBehavior<CreateProductCommand, Product>>> loggerMock = new();

            CreateProductCommand request = new(product);

            Product response = new()
            {
                ProductId = 2,
                Name = "Second",
                Price = 2,
                CategoryId = 2,
            };

            var loggingBehavior = new LoggingBehavior<CreateProductCommand, Product>(loggerMock.Object);

            // Act
            var result = await loggingBehavior.Handle(request,
                () => Task.FromResult(response), CancellationToken.None);

            // Assert

            result.Should().Be(response);
        }
    }
}
