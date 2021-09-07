using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace Coupon.API.IntegrationEvents.Events
{
	public record OrderCouponRejectedIntegrationEvent(int OrderId, string Code) : IntegrationEvent;
}