using System.Threading.Tasks;
using Coupon.API.IntegrationEvents.Events;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;

namespace Coupon.API.IntegrationEvents.EventHandlers
{
	public class OrderStatusChangedToAwaitingCouponValidationIntegrationEventHandler: IIntegrationEventHandler<OrderStatusChangedToAwaitingCouponValidationIntegrationEvent>
	{
		public Task Handle(OrderStatusChangedToAwaitingCouponValidationIntegrationEvent @event)
		{
			throw new System.NotImplementedException();
		}
	}
}