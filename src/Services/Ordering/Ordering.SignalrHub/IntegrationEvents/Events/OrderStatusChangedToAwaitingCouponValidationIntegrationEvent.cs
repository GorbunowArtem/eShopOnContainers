using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
using Newtonsoft.Json;

namespace Ordering.SignalrHub.IntegrationEvents.Events
{
    public record OrderStatusChangedToAwaitingCouponValidationIntegrationEvent : IntegrationEvent
    {
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
