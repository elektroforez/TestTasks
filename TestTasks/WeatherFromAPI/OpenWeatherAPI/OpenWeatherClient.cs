using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestTasks.WeatherFromAPI.OpenWeatherAPI
{
    public class OpenWeatherClient : IDisposable
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;
        private readonly bool _useHttps;
        public OpenWeatherClient(string apiKey, bool useHttps = false)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient();
            _useHttps = useHttps;
        }

        private async Task<Uri> GetRequestUri(string queryString, string time)
        {
            var geo = await GetLocation(queryString);

            return new Uri($"https://api.openweathermap.org/data/3.0/onecall/timemachine?lat={geo.Lat}&lon={geo.Lon}&dt={time}&appid={_apiKey}");
        }

        public async Task<GeoResponse> GetLocation(string queryString)
        {
            var jsonResponse = await _httpClient.GetStringAsync($"http://api.openweathermap.org/geo/1.0/direct?q={queryString}&limit=1&appid={_apiKey}");
            return new GeoResponse(jsonResponse);
        }

        public async Task<QueryResponse> Query(string queryString, string time)
        {
            var jsonResponse = await _httpClient.GetStringAsync(GetRequestUri(queryString, time).Result);
            var query = new QueryResponse(jsonResponse);
            return query.Success ? query : null;
        }

        #region IDisposable
        private bool _disposed;

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // dispose managed state (managed objects).
            }

            // free unmanaged resources (unmanaged objects) and override a finalizer below.
            // set large fields to null.

            _httpClient.Dispose();

            _disposed = true;
        }

        #endregion
    }
}
