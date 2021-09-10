using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Coupon.API;
using Coupon.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Xunit;

namespace Coupon.FunctionalTests
{
	public class CustomWebApplicationFactory<TStartup>
		: WebApplicationFactory<TStartup> where TStartup: class
	{
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.ConfigureServices(services =>
			{
				var sp = services.BuildServiceProvider();

				using (var scope = sp.CreateScope())
				{
					var scopedServices = scope.ServiceProvider;
					var db = scopedServices.GetRequiredService<CouponContext>();
					var logger = scopedServices
						.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();
				}
			});
		}
	}
	
	
	public class CouponTutorial
	{
		private readonly CustomWebApplicationFactory<Startup> _factory;

		public CouponTutorial()
		{
			_factory = new CustomWebApplicationFactory<Startup>();
		}


		[Fact]
		public async Task ShouldGet()
		{
			// Arrange
			var client = _factory.WithWebHostBuilder(builder =>
			{
				builder.ConfigureTestServices(services =>
				{
					services.AddAuthentication(options =>
						{
							options.DefaultAuthenticateScheme = TestAuthHandler.DefaultScheme;
							options.DefaultScheme = TestAuthHandler.DefaultScheme;
						})
						.AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
							TestAuthHandler.DefaultScheme, options => { });

				});

			})
				.CreateClient(new WebApplicationFactoryClientOptions
				{
					AllowAutoRedirect = false,
				});

			client.DefaultRequestHeaders.Authorization = 
				new AuthenticationHeaderValue("Test");


			var response = await client.GetAsync("api/v1/coupon/DISC-10");

			response.EnsureSuccessStatusCode();
		}
	}
}