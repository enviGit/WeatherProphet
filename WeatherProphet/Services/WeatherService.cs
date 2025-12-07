using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WeatherProphet.Models;

namespace WeatherProphet.Services
{
    public class WeatherService
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public WeatherService(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient();
        }

        public async Task<WeatherForecast> GetCurrentWeatherAsync(string city, string langCode)
        {
            string url = $"http://api.openweathermap.org/data/2.5/weather?q={city}&lang={langCode}&appid={_apiKey}&units=metric";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<WeatherApiResponse>(url);

                if (response == null) return null;

                return new WeatherForecast(
                     DateTime.Now,
                     response.Weather.FirstOrDefault()?.Description ?? "",
                     response.Main.Temp,
                     $"http://openweathermap.org/img/w/{response.Weather.FirstOrDefault()?.Icon}.png",
                     TranslationService.GetDayOfWeek(DateTime.Now, langCode)
                 );
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<WeatherForecast>> GetForecastAsync(string city, int days, string langCode)
        {
            string url = $"http://api.openweathermap.org/data/2.5/forecast?q={city}&lang={langCode}&appid={_apiKey}&units=metric";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<ForecastApiResponse>(url);
                if (response?.List == null) return new List<WeatherForecast>();

                var forecasts = new List<WeatherForecast>();

                var dailyForecasts = response.List
                    .Where(x => !string.IsNullOrEmpty(x.DtTxt))
                    .GroupBy(x => DateTime.Parse(x.DtTxt).Date)
                    .Skip(1)
                    .Take(days);

                foreach (var dayGroup in dailyForecasts)
                {
                    var item = dayGroup.OrderBy(x => Math.Abs(DateTime.Parse(x.DtTxt).Hour - 12)).First();

                    DateTime date = DateTime.Parse(item.DtTxt);

                    string dayName = TranslationService.GetDayOfWeek(date, langCode);

                    forecasts.Add(new WeatherForecast(
                        date,
                        item.Weather.FirstOrDefault()?.Description ?? "",
                        item.Main.Temp,
                        $"http://openweathermap.org/img/w/{item.Weather.FirstOrDefault()?.Icon}.png",
                        dayName
                    ));
                }

                return forecasts;
            }
            catch
            {
                return new List<WeatherForecast>();
            }
        }
    }
}