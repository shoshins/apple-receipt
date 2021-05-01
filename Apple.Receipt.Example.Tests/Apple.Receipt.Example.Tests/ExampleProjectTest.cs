using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;

namespace Apple.Receipt.Example.Tests
{
    public class ExampleProjectTest
    {
        private readonly HttpClient _httpClient;
        
        public ExampleProjectTest()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development").UseStartup<Startup>()
            );
            _httpClient = server.CreateClient();
        }
        
        [Theory]
        [InlineData("GET")]
        public async Task Get(string method)
        {
            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod(method), "/weatherforecast");
            HttpResponseMessage response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, HttpStatusCode.OK);
            
            string responseContent = await response.Content.ReadAsStringAsync();
            WeatherForecast[] weatherForecasts = JsonConvert.DeserializeObject<WeatherForecast[]>(responseContent);
            Assert.Equal(5, weatherForecasts.Length);
            const string correctAppleReceiptResponse =
                "== AppleReceiptLibrary == Message: IAP receipt verification failed; Status: NotAuthenticatedReceipt; ";
            Assert.Equal(weatherForecasts.First().AppleReceiptWeather, correctAppleReceiptResponse);
        }
    }
}