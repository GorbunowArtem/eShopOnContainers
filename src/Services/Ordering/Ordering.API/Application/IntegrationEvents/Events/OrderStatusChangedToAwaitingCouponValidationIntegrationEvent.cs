using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
using Newtonsoft.Json;

namespace Ordering.API.Application.IntegrationEvents.Events
{
	public record OrderStatusChangedToAwaitingCouponValidationIntegrationEvent : IntegrationEvent
	{
		public OrderStatusChangedToAwaitingCouponValidationIntegrationEvent(int orderId, string orderStatus,
			string buyerName,
			string code)
		{
			OrderId = orderId;
			OrderStatus = orderStatus;
			BuyerName = buyerName;
			Code = code;
		}

		[JsonProperty]
		public int OrderId { get; set; }

		[JsonProperty]
		public string OrderStatus { get; set; }

		[JsonProperty]
		public string BuyerName { get; set; }

		[JsonProperty]
		public string Code { get; set; }
	}
}