using MediatR;
using SimpleMediatr.MediatR.Notifications;
using SimpleMediatr.Services.Interfaces;

namespace SimpleMediatr.MediatR.Handlers
{
    public sealed class CacheInvalidationHandler : INotificationHandler<ProductAddedNotification>
    {
        private readonly IProductService _productService;

        public CacheInvalidationHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task Handle(ProductAddedNotification notification, CancellationToken cancellationToken)
        {

            await _productService.EventOccured(notification.Product, "Cache Invalidated");

            await Task.CompletedTask;
        }
    }
}
