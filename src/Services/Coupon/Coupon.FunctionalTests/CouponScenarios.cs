using System.Net;
using System.Threading.Tasks;
using Coupon.FunctionalTests.Base;
using FluentAssertions;
using Xunit;

namespace Coupon.FunctionalTests
{
	public class CouponScenarios : CouponScenarioBase
	{
		[Fact]
		public async Task ShouldReturnCouponByCode()
		{
			using var server = CreateServer();

			var response = await server.CreateClient()
				.GetAsync("api/v1/coupon/DISC-10");

			response.EnsureSuccessStatusCode();
		}

		[Fact]
		public async Task ShouldReturnNotFoundIfCouponNotExists()
		{
			using var server = CreateServer();

			var response = await server.CreateClient()
				.GetAsync("api/v1/coupon/DISC-100");

			response.StatusCode.Should().Be(HttpStatusCode.NotFound);
		}
	}
}