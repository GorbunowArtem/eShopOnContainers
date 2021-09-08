using System.Threading.Tasks;
using AutoFixture;
using Coupon.API.Infrastructure.Repositories;
using Coupon.API.IntegrationEvents.EventHandlers;
using Coupon.API.IntegrationEvents.Events;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Moq;
using Xunit;

namespace Coupon.UnitTests.IntegrationEvents.EventHandlers
{
	public class OrderStatusChangedToAwaitingCouponValidationIntegrationEventHandlerTests
	{
		private const string NonExistingCouponCode = "Non exist";
		private const string ConsumedCouponCode = "Consumed";
		private const string ValidCouponCode = "Valid";

		private readonly Fixture _fixture;
		private readonly OrderStatusChangedToAwaitingCouponValidationIntegrationEventHandler _sut;
		private readonly Mock<IEventBus> _eventBusMock;
		private readonly Mock<ICouponRepository> _couponRepoStub;

		[Theory]
		[InlineData(ConsumedCouponCode)]
		[InlineData(NonExistingCouponCode)]
		public async Task ShouldPublishOrderCouponRejectedIntegrationEventIfCouponIsNullOrConsumed(string couponCode)
		{
			await _sut.Handle(_fixture.Build<OrderStatusChangedToAwaitingCouponValidationIntegrationEvent>()
				.With(c => c.Code, couponCode)
				.Create());

			_eventBusMock.Verify(eb => eb.Publish(It.IsAny<OrderCouponRejectedIntegrationEvent>()));
		}

		[Fact]
		public async Task ShouldPublishOrderCouponConfirmedIntegrationEventIfCouponIsValid()
		{
			await _sut.Handle(_fixture.Build<OrderStatusChangedToAwaitingCouponValidationIntegrationEvent>()
				.With(c => c.Code, ValidCouponCode)
				.Create());

			_eventBusMock.Verify(eb => eb.Publish(It.IsAny<OrderCouponConfirmedIntegrationEvent>()));
		}

		[Fact]
		public async Task ShouldUpdateCouponConsumedIfCouponIsValid()
		{
			var expectedEvent = _fixture.Build<OrderStatusChangedToAwaitingCouponValidationIntegrationEvent>()
				.With(c => c.Code, ValidCouponCode)
				.Create();

			await _sut.Handle(expectedEvent);

			_couponRepoStub.Verify(eb =>
				eb.UpdateCouponConsumedByCodeAsync(It.Is<string>(c => c.Equals(expectedEvent.Code)),
					It.Is<int>(o => o == expectedEvent.OrderId)));
		}

		public OrderStatusChangedToAwaitingCouponValidationIntegrationEventHandlerTests()
		{
			_fixture = new Fixture();

			_eventBusMock = new Mock<IEventBus>();

			_couponRepoStub = new Mock<ICouponRepository>();

			_couponRepoStub.Setup(cr => cr.FindCouponByCodeAsync(It.Is<string>(c => c.Equals(NonExistingCouponCode))))
				.ReturnsAsync(() => null);

			_couponRepoStub.Setup(cr => cr.FindCouponByCodeAsync(It.Is<string>(c => c.Equals(ConsumedCouponCode))))
				.ReturnsAsync(_fixture.Build<API.Infrastructure.Models.Coupon>()
					.With(c => c.Consumed, true)
					.Create());

			_couponRepoStub.Setup(cr => cr.FindCouponByCodeAsync(It.Is<string>(c => c.Equals(ValidCouponCode))))
				.ReturnsAsync(_fixture.Build<API.Infrastructure.Models.Coupon>()
					.With(c => c.Consumed, false)
					.Create());

			_sut = new OrderStatusChangedToAwaitingCouponValidationIntegrationEventHandler(_couponRepoStub.Object,
				_eventBusMock.Object);
		}
	}
}