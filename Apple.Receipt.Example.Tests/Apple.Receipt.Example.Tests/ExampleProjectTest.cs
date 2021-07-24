using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Apple.Receipt.Models;
using Apple.Receipt.Verificator.Models;
using Apple.Receipt.Verificator.Models.IAPVerification;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
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
                .UseEnvironment("Development")
                .UseConfiguration(new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build())
                .UseStartup<Startup>()
            );
            _httpClient = server.CreateClient();
        }
        
        [Fact]
        public async Task VerificatorUsageTest()
        {
            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("GET"), "/apple-receipt/verificator");
            HttpResponseMessage response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, HttpStatusCode.OK);
            
            string responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("IAP receipt verification failed", responseContent, StringComparison.InvariantCultureIgnoreCase);
        }
        
        [Fact]
        public async Task ParserUsageTest()
        {
            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("GET"), "/apple-receipt/parser");
            HttpResponseMessage response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, HttpStatusCode.OK);
            
            string responseContent = await response.Content.ReadAsStringAsync();
            AppleAppReceipt parserResult = JsonConvert.DeserializeObject<AppleAppReceipt>(responseContent);
            Assert.Equal("com.mbaasy.ios.demo", parserResult.BundleId);
        }
    }
}