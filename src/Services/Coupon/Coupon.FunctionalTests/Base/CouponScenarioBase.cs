using System.IO;
using System.Reflection;
using Coupon.API;
using Coupon.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Coupon.FunctionalTests.Base
{
	public class CouponScenarioBase
	{
		protected TestServer CreateServer()
		{
			var path = Assembly.GetAssembly(typeof(CouponContext)).Location;

			var hostBuilder = new WebHostBuilder()
				.UseContentRoot(Path.GetDirectoryName(path))
				.ConfigureAppConfiguration(cb =>
				{
					cb.AddJsonFile("appsettings.json", optional: false)
						.AddEnvironmentVariables();
				})
				.ConfigureTestServices(services =>
				{
					services.AddAuthentication(options =>
						{
							options.DefaultAuthenticateScheme = TestAuthHandler.DefaultScheme;
							options.DefaultScheme = TestAuthHandler.DefaultScheme;
						})
						.AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
							TestAuthHandler.DefaultScheme, options => { });

				})
				.UseStartup<Startup>();


			var testServer = new TestServer(hostBuilder);
			
			return testServer;
		}
	}
}