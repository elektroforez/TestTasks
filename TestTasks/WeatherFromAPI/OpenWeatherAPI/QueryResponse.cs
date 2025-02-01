using System;
using Newtonsoft.Json.Linq;

namespace TestTasks.WeatherFromAPI.OpenWeatherAPI
{
    public class QueryResponse
    {
        public bool Success { get; set; }
        public Temperature Temperature { get; set; }
        public Rain Rain { get; set; }

        public DateTime Date { get; set; }

        public Coordinates Coordinates { get; set; }

        public QueryResponse(string jsonResponse)
        {
            var jsondata =  JObject.Parse(jsonResponse);
            if (jsondata != null)
            {
                Temperature = new Temperature(jsondata.SelectToken("data[0]"));
                Rain = new Rain(jsondata.SelectToken("data[0]"));
                Date = UnixTimeStampToDateTime(double.Parse(jsondata.SelectToken("data[0].dt").ToString()));
                Coordinates = new Coordinates(jsondata);
                Success = true;
            }
            else
            {
                Success = false;
            }
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
    }
}
