using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TestTasks.WeatherFromAPI.OpenWeatherAPI
{
    public class Rain
    {
        public double volume { get; }

        public Rain(JToken data)
        {
            if(data == null) throw new ArgumentNullException("data");
            if(data.SelectToken("1h") != null)
            {
                volume = double.Parse(data.SelectToken("1h").ToString());
            }
        }
    }
}
