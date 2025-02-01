using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TestTasks.WeatherFromAPI.Models;

namespace TestTasks.WeatherFromAPI
{
    public class WeatherManager
    {

        private readonly OpenWeatherAPI.OpenWeatherClient _openWeatherClient = new OpenWeatherAPI.OpenWeatherClient("api_key");
        public async Task<WeatherComparisonResult> CompareWeather(string cityA, string cityB, int dayCount)
        {
            var days = GetDays(dayCount);
            int warmerDays = 0;
            int rainyDays = 0;
            
            for(int i = 0; i < dayCount; i++)
            {
                var responseA = _openWeatherClient.Query(cityA, days[i]);
                var responseB = _openWeatherClient.Query(cityB, days[i]);

                if (responseA == null || responseB == null)
                {
                    return null;
                }

                if(responseA.Result.Temperature.TempKel > responseB.Result.Temperature.TempKel)
                {
                    warmerDays++;
                }
                if(responseA.Result.Rain.volume > responseB.Result.Rain.volume)
                {
                    rainyDays++;
                }
            }
            return new WeatherComparisonResult(cityA, cityB, warmerDays, rainyDays);
        }

        private string[] GetDays(int count)
        {
            string[] unixDays = new string[count];
            for(int i = 0; i < count; i++)
            {
                var day = DateTime.Today.AddDays(0 - i);
                unixDays[i] = ((DateTimeOffset)day).ToUnixTimeSeconds().ToString();
            }

            return unixDays;
        }
    }
}
