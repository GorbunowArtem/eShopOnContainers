using MediatR;

namespace Ordering.Domain.Events
{
	public record OrderStatusChangedToValidatedDomainEvent(int OrderId) : INotification;
}