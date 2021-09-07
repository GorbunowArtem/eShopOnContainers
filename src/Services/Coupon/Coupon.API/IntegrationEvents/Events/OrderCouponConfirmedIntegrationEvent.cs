using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace Coupon.API.IntegrationEvents.Events
{
	public record OrderCouponConfirmedIntegrationEvent(int OrderId, int Discount) : IntegrationEvent;
}