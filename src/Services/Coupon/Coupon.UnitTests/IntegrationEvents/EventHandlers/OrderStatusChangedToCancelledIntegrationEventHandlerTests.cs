using System.Threading.Tasks;
using AutoFixture;
using Coupon.API.Infrastructure.Repositories;
using Coupon.API.IntegrationEvents.EventHandlers;
using Coupon.API.IntegrationEvents.Events;
using Moq;
using Xunit;

namespace Coupon.UnitTests.IntegrationEvents.EventHandlers
{
	public class OrderStatusChangedToCancelledIntegrationEventHandlerTests
	{
		private readonly Fixture _fixture;

		public OrderStatusChangedToCancelledIntegrationEventHandlerTests()
		{
			_fixture = new Fixture();
		}
		
		[Fact]
		public async Task ShouldReleaseCoupon()
		{
			var couponRepoMock = new Mock<ICouponRepository>();
			var sut = new OrderStatusChangedToCancelledIntegrationEventHandler(couponRepoMock.Object);
			var expected = _fixture.Create<OrderStatusChangedToCancelledIntegrationEvent>();

			await sut.Handle(expected);

			couponRepoMock.Verify(c => c.UpdateCouponReleasedByOrderIdAsync(It.Is<int>(v => expected.OrderId == v)));
		}
	}
}