using System.Collections.Generic;
using System.Threading.Tasks;
using Coupon.API;
using Coupon.API.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using Xunit;

namespace Coupon.UnitTests.Infrastructure.Repositories
{
	public class CouponRepositoryTests
	{
		private const string Coupon1Code = "DISC-1";
		private const string Coupon2Code = "DISC-2";
		private readonly CouponRepository _sut;
		private readonly CouponContext _couponContext;

		[Fact]
		public async Task ShouldUpdateCouponConsumed()
		{
			const int expectedOrderId = 2;

			await _sut.UpdateCouponConsumedByCodeAsync(Coupon1Code, expectedOrderId);

			var actual = await _sut.FindCouponByCodeAsync(Coupon1Code);

			actual.Consumed.Should().BeTrue();
			actual.OrderId.Should().Be(expectedOrderId);
		}

		[Fact]
		public async Task ShouldUpdateCouponReleased()
		{
			await _sut.UpdateCouponReleasedByOrderIdAsync(2);

			var actual = await _sut.FindCouponByCodeAsync(Coupon2Code);

			actual.Consumed.Should().BeFalse();
			actual.OrderId.Should().Be(0);
		}

		public CouponRepositoryTests()
		{
			var settingsParameter = new Mock<IOptions<CouponSettings>>();
			settingsParameter.Setup(sp => sp.Value)
				.Returns(new CouponSettings
				{
					ConnectionString = "mongodb://localhost:27017",
					CouponMongoDatabase = "CouponTests"
				});

			_couponContext = new CouponContext(settingsParameter.Object);
			_couponContext.Coupons.DeleteMany(FilterDefinition<API.Infrastructure.Models.Coupon>.Empty);

			_couponContext.Coupons.InsertMany(new List<API.Infrastructure.Models.Coupon>
			{
				new()
				{
					Code = Coupon1Code,
					Discount = 5,
					Consumed = false,
				},
				new()
				{
					Code = Coupon2Code,
					Discount = 10,
					Consumed = true,
					OrderId = 2
				}
			});

			_sut = new CouponRepository(_couponContext);
		}
	}
}