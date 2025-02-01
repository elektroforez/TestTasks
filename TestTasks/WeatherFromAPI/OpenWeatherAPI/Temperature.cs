using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TestTasks.WeatherFromAPI.OpenWeatherAPI
{
    public class Temperature
    {
        public double TempKel {get;}

        public Temperature(JToken data)
        {
            if (data == null) throw new ArgumentNullException("data");
            
            TempKel = double.Parse(data.SelectToken("temp").ToString());
        }
    }
}
