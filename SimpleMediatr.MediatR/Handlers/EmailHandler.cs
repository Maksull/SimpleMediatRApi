using MediatR;
using SimpleMediatr.MediatR.Notifications;
using SimpleMediatr.Services.Interfaces;

namespace SimpleMediatr.MediatR.Handlers
{
    public sealed class EmailHandler : INotificationHandler<ProductAddedNotification>
    {
        private readonly IProductService _productService;

        public EmailHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task Handle(ProductAddedNotification notification, CancellationToken cancellationToken)
        {
            await _productService.EventOccured(notification.Product, "Email sent");

            await Task.CompletedTask;
        }
    }
}
