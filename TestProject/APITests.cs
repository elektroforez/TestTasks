using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;
using TestTasks.WeatherFromAPI.OpenWeatherAPI;

namespace TestProject
{
    public class APITests
    {
        private const string apiKey = "api_key";

        private readonly ITestOutputHelper output;

        public APITests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public async Task QueryTest_Success()
        {
            var api = new OpenWeatherClient(apiKey);

            var actual = await api.Query("Kyiv,ua", DateTimeOffset.Now.ToUnixTimeSeconds().ToString());

            Assert.True(actual.Success);

            output.WriteLine(JsonConvert.SerializeObject(actual, Formatting.Indented));
        }

        [Fact]
        public async Task Geolocate_Success()
        {
            var api = new OpenWeatherClient(apiKey);

            var actual = await api.GetLocation("lviv,ua");

            Assert.True(actual.Success);

            output.WriteLine(JsonConvert.SerializeObject(actual, Formatting.Indented));
        }

    }
}
