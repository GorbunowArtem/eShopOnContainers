using System.IO;
using System.Reflection;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Coupon.API;
using Coupon.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Coupon.FunctionalTests
{
	public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
	{
		public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, 
			ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
			: base(options, logger, encoder, clock)
		{
		}

		protected override Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			var claims = new[] { new Claim(ClaimTypes.Name, "Test user") };
			var identity = new ClaimsIdentity(claims, "Test");
			var principal = new ClaimsPrincipal(identity);
			var ticket = new AuthenticationTicket(principal, "Test");

			var result = AuthenticateResult.Success(ticket);

			return Task.FromResult(result);
		}
	}
	public class CouponScenarioBase
	{
		public TestServer CreateServer()
		{
			var path = Assembly.GetAssembly(typeof(CouponContext)).Location;

			var hostBuilder = new WebHostBuilder()
				.UseContentRoot(Path.GetDirectoryName(path))
				.ConfigureAppConfiguration(cb =>
				{
					cb.AddJsonFile("appsettings.json", optional: false)
						.AddEnvironmentVariables();
				})
				.ConfigureServices(services =>
				{
					services.AddAuthentication("Test")
						.AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
							"Test", options => {});
				})
				.UseStartup<Startup>();


			var testServer = new TestServer(hostBuilder);
			
			return testServer;
		}
	}
}