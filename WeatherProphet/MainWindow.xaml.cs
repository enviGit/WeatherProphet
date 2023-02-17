using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

#nullable disable

namespace WeatherProphet
{
    public partial class MainWindow : Window
    {
        private readonly string apiKey;
        private readonly HttpClient httpClient;

        public MainWindow()
        {
            InitializeComponent();
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
            apiKey = config["OpenWeatherApiKey"];
            httpClient = new HttpClient();
        }
        private async Task<List<WeatherForecast>> GetWeatherForecasts(string city, int numDays)
        {
            string forecastUrl = $"http://api.openweathermap.org/data/2.5/forecast?q={city}&appid={apiKey}&units=metric";

            try
            {
                HttpResponseMessage forecastResponse = await httpClient.GetAsync(forecastUrl);
                forecastResponse.EnsureSuccessStatusCode();
                string forecastResponseBody = await forecastResponse.Content.ReadAsStringAsync();
                dynamic forecastResult = JsonConvert.DeserializeObject(forecastResponseBody);
                List<WeatherForecast> forecasts = new List<WeatherForecast>();

                for (int i = 0; i < numDays; i++)
                {
                    DateTime date = forecastResult.list[i].dt_txt;
                    string weather = forecastResult.list[i].weather[0].description;
                    double temperature = forecastResult.list[i].main.temp;
                    string imageUrl = $"http://openweathermap.org/img/w/{forecastResult.list[i].weather[0].icon}.png";


                    forecasts.Add(new WeatherForecast(date, weather, temperature, imageUrl));
                }

                return forecasts;
            }
            catch (HttpRequestException ex)
            {
                textBlockWeather.Text = $"Error: {ex.Message}";

                return null;
            }
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            string city = textBoxCity.Text;
            string weatherUrl = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";
            string forecastUrl = $"http://api.openweathermap.org/data/2.5/forecast?q={city}&appid={apiKey}&units=metric";

            try
            {
                HttpResponseMessage weatherResponse = await httpClient.GetAsync(weatherUrl);
                weatherResponse.EnsureSuccessStatusCode();
                string weatherResponseBody = await weatherResponse.Content.ReadAsStringAsync();
                dynamic weatherResult = JsonConvert.DeserializeObject(weatherResponseBody);
                string weather = weatherResult?.weather?[0]?.description;
                double temperature = Math.Round((double)weatherResult.main.temp, 1);
                string temperatureString = temperature.ToString("0.0", CultureInfo.InvariantCulture);

                if (weather != null && temperature != null)
                {
                    textBlockWeather.Text = $"Current weather in {city}: {weather}, {temperatureString}°C";

                    HttpResponseMessage forecastResponse = await httpClient.GetAsync(forecastUrl);
                    forecastResponse.EnsureSuccessStatusCode();
                    string forecastResponseBody = await forecastResponse.Content.ReadAsStringAsync();
                    dynamic forecastResult = JsonConvert.DeserializeObject(forecastResponseBody);
                    var dailyForecasts = ((IEnumerable<dynamic>)forecastResult.list).Where(x => DateTime.Parse(x.dt_txt.ToString()).TimeOfDay == TimeSpan.FromHours(12)).ToList();
                    List <WeatherForecast> forecasts = new List<WeatherForecast>();

                    foreach(var item in dailyForecasts)
                    {
                        DateTime date = item.dt_txt;
                        string forecastWeather = item.weather[0].description;
                        double forecastTemperature = item.main.temp;
                        string imageUrl = $"http://openweathermap.org/img/w/{item.weather[0].icon}.png";
                        forecasts.Add(new WeatherForecast(date, forecastWeather, forecastTemperature, imageUrl));
                    }

                    if (forecasts != null && forecasts.Any())
                    {
                        listBoxForecast.ItemsSource = forecasts;
                        listBoxForecast.Visibility = Visibility.Visible;
                        textBlockForecast.Text = string.Empty;
                    }
                    else
                    {
                        textBlockForecast.Text = "Weather forecast not available.";
                        listBoxForecast.Visibility = Visibility.Collapsed;
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                textBlockWeather.Text = $"Error: {ex.Message}";
                listBoxForecast.Visibility = Visibility.Collapsed;
            }
        }
    }
}