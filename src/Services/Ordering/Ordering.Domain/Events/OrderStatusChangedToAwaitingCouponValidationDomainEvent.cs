using MediatR;

namespace Ordering.Domain.Events
{
	public record OrderStatusChangedToAwaitingCouponValidationDomainEvent(int OrderId, string Code) : INotification;
}