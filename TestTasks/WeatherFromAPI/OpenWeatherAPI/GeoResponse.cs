using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TestTasks.WeatherFromAPI.OpenWeatherAPI
{
    public class GeoResponse
    {
        public double Lat { get; }
        public double Lon { get; }
        public bool Success { get; }

        public GeoResponse(string jsonResponse)
        {
            var jsonData = JArray.Parse(jsonResponse);
            if (jsonData != null)
            {
                Lat = double.Parse(jsonData[0].SelectToken("lat").ToString(), CultureInfo.CurrentCulture);
                Lon = double.Parse(jsonData[0].SelectToken("lon").ToString(), CultureInfo.CurrentCulture);
                Success = true;
            }
            else
            {
                Success = false;
            }
        }
    }
}
