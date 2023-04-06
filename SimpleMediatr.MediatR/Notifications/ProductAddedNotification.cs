using MediatR;
using SimpleMediatr.Models;

namespace SimpleMediatr.MediatR.Notifications
{
    public sealed record ProductAddedNotification(Product Product) : INotification;
}
