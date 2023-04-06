using SimpleMediatr.MediatR.Handlers;
using SimpleMediatr.MediatR.Notifications;
using SimpleMediatr.Models;
using SimpleMediatr.Services.Interfaces;

namespace SimpleMediatr.MediatR.Tests.Handlers
{
    public sealed class CacheInvalidationHandlerTests
    {
        [Fact]
        public async Task Handle_WhenCalled_CallEventOccured_With_CacheMessage()
        {
            // Arrange
            Mock<IProductService> productService = new();
            ProductAddedNotification notification = new(new Product());

            CacheInvalidationHandler cacheInvalidationHandler = new (productService.Object);

            // Act
            await cacheInvalidationHandler.Handle(notification, CancellationToken.None);

            // Assert
            productService.Verify(
                x => x.EventOccured(notification.Product, "Cache Invalidated"),
                Times.Once);

            productService.VerifyNoOtherCalls();
        }
    }
}
