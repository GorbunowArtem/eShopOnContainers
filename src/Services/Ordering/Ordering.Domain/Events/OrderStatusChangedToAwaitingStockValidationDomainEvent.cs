namespace Ordering.Domain.Events
{
	using MediatR;
	using Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.OrderAggregate;
	using System.Collections.Generic;

	/// <summary>
	/// Event used when the grace period order is confirmed
	/// </summary>
	public record OrderStatusChangedToAwaitingStockValidationDomainEvent
		(int OrderId, IEnumerable<OrderItem> OrderItems) : INotification;
}