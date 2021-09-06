using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.eShopOnContainers.Web.Shopping.HttpAggregator.Config;
using Microsoft.Extensions.Logging;

namespace Microsoft.eShopOnContainers.Web.Shopping.HttpAggregator.Services
{
	public class CouponService: ICouponService
	{
		private readonly HttpClient _httpClient;
		private readonly UrlsConfig _urls;
		private readonly ILogger<CouponService> _logger;
		public CouponService(HttpClient httpClient, UrlsConfig urls,
			ILogger<CouponService> logger)
		{
			_httpClient = httpClient;
			_urls = urls;
			_logger = logger;
		}

		public async Task<HttpResponseMessage> CheckCouponByCodeNumberAsync(string codeNumber)
		{
			_logger.LogInformation("Call coupon api with codenumber: {codeNumber}", codeNumber);

			var url = new Uri($"{_urls.Coupon}/api/v1/coupon/{codeNumber}");

			var response = await _httpClient.GetAsync(url);

			_logger.LogInformation("Coupon api response: {@response}", response);

			return response;
		}
	}
}