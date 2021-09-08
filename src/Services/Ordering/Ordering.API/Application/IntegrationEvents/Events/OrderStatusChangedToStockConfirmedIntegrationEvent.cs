namespace Ordering.API.Application.IntegrationEvents.Events
{
    using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

    public record OrderStatusChangedToStockConfirmedIntegrationEvent(
        int OrderId,
        string OrderStatus,
        string BuyerName,
        decimal Total) : IntegrationEvent;
}