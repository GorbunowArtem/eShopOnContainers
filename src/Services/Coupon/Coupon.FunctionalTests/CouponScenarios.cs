using System.Threading.Tasks;
using Xunit;

namespace Coupon.FunctionalTests
{
	public class CouponScenarios: CouponScenarioBase
	{
		[Fact]
		public async Task ShouldReturnCouponByCode()
		{
			using var server = CreateServer();
			
			var response = await server.CreateClient()
				.GetAsync("api/v1/coupon/DISC-10");

			response.EnsureSuccessStatusCode();
		}
	}
}