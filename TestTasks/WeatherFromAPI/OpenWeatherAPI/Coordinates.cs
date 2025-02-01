using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TestTasks.WeatherFromAPI.OpenWeatherAPI
{
    public class Coordinates
    {
        public double Latitude { get; }
        public double Longitude { get; }

        public Coordinates(JToken data)
        {
            if (data == null) throw new ArgumentNullException("data");
            Latitude = double.Parse(data.SelectToken("lat").ToString());
            Longitude = double.Parse(data.SelectToken("lon").ToString());

        }
    }
}
