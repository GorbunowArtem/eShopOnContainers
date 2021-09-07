using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace Ordering.API.Application.IntegrationEvents.Events
{
	public record OrderStatusChangedToAwaitingCouponValidationIntegrationEvent(
		int OrderId,
		string OrderStatus,
		string BuyerName,
		string Code) : IntegrationEvent;
}