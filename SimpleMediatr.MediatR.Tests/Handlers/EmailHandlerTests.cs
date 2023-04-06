using SimpleMediatr.MediatR.Handlers;
using SimpleMediatr.MediatR.Notifications;
using SimpleMediatr.Models;
using SimpleMediatr.Services.Interfaces;

namespace SimpleMediatr.MediatR.Tests.Handlers
{
    public sealed class EmailHandlerTests
    {
        [Fact]
        public async Task Handle_WhenCalled_CallEventOccured_With_EmailMessage()
        {
            // Arrange
            Mock<IProductService> productServiceMock = new();
            ProductAddedNotification notification = new(new Product());

            EmailHandler emailHandler = new (productServiceMock.Object);

            // Act
            await emailHandler.Handle(notification, CancellationToken.None);

            // Assert
            productServiceMock.Verify(
                x => x.EventOccured(notification.Product, "Email sent"),
                Times.Once);

            productServiceMock.VerifyNoOtherCalls();
        }
    }
}
