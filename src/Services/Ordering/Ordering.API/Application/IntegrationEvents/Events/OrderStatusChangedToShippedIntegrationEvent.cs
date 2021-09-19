using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace Ordering.API.Application.IntegrationEvents.Events
{
	public record OrderStatusChangedToShippedIntegrationEvent(
		int OrderId,
		string OrderStatusName,
		string BuyerName,
		int? BuyerId,
		decimal PaidAmount) : IntegrationEvent;
}