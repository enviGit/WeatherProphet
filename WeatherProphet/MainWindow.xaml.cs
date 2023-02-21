using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
        private async Task GetWeather(string city)
        {
            string weatherUrl = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

            try
            {
                HttpResponseMessage weatherResponse = await httpClient.GetAsync(weatherUrl);
                weatherResponse.EnsureSuccessStatusCode();
                string weatherResponseBody = await weatherResponse.Content.ReadAsStringAsync();
                dynamic weatherResult = JsonConvert.DeserializeObject(weatherResponseBody);
                string weather = weatherResult.weather[0].description;
                double temperature = Math.Round((double)weatherResult.main.temp, 1);
                string temperatureString = temperature.ToString("0.0", CultureInfo.InvariantCulture);

                if (weather != null && temperature != null)
                    textBlockWeather.Text = $"Current weather in {city}: {weather}, {temperatureString}°C";


            }
            catch (HttpRequestException ex)
            {
                textBlockWeather.Text = $"Error: {ex.Message}";
                listBoxForecast.Visibility = Visibility.Collapsed;
            }
        }
        private async Task<List<WeatherForecast>> GetWeatherForecasts(string city, int numDays, bool useSelectedValue = true)
        {
            if (useSelectedValue && comboBoxDaysToShow.SelectedItem != null)
            {
                string forecastUrl = $"http://api.openweathermap.org/data/2.5/forecast?q={city}&appid={apiKey}&units=metric";

                try
                {
                    HttpResponseMessage forecastResponse = await httpClient.GetAsync(forecastUrl);
                    forecastResponse.EnsureSuccessStatusCode();
                    string forecastResponseBody = await forecastResponse.Content.ReadAsStringAsync();
                    dynamic forecastResult = JsonConvert.DeserializeObject(forecastResponseBody);
                    JArray forecastList = (JArray)forecastResult.list;
                    List<WeatherForecast> forecasts = new List<WeatherForecast>();

                    foreach (var group in forecastList.GroupBy(x => ((DateTime)x["dt_txt"]).Date).Skip(1).Take(numDays))
                    {
                        var item = group.FirstOrDefault(x => ((DateTime)x["dt_txt"]).Hour == 12);

                        if (item == null)
                            continue;

                        DateTime date = item["dt_txt"].ToObject<DateTime>();
                        string weather = item["weather"][0]["description"].ToString();
                        double temperature = item["main"]["temp"].ToObject<double>();
                        string imageUrl = $"http://openweathermap.org/img/w/{item["weather"][0]["icon"].ToString()}.png";
                        forecasts.Add(new WeatherForecast(date, weather, temperature, imageUrl));
                    }

                    return forecasts;
                }
                catch (HttpRequestException ex)
                {
                    textBlockForecast.Text = $"Error: {ex.Message}";

                    return null;
                }
            }

            return new List<WeatherForecast>();
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string city = textBoxCity.Text;

            try
            {
                await GetWeather(city);

                if (comboBoxDaysToShow.SelectedItem != null)
                {
                    int numDays = Convert.ToInt32(((ComboBoxItem)comboBoxDaysToShow.SelectedItem).Content);
                    var forecasts = await GetWeatherForecasts(city, numDays);

                    if (forecasts != null && forecasts.Count > 0)
                    {
                        listBoxForecast.ItemsSource = forecasts;
                        listBoxForecast.Visibility = Visibility.Visible;
                        textBlockForecast.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        listBoxForecast.Visibility = Visibility.Collapsed;
                        textBlockForecast.Text = "No forecast available.";
                        textBlockForecast.Visibility = Visibility.Visible;
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                textBlockWeather.Text = $"Error: {ex.Message}";
                listBoxForecast.Visibility = Visibility.Collapsed;
            }
        }
        private void Button_ToggleTheme(object sender, RoutedEventArgs e)
        {
            if (Resources.MergedDictionaries.Count > 0 && Resources.MergedDictionaries[0].Source.OriginalString == "Themes/DarkTheme.xaml")
                Resources.MergedDictionaries.Clear();
            else
            {
                Resources.MergedDictionaries.Clear();
                Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("Themes/DarkTheme.xaml", UriKind.Relative) });
            }
        }
    }
}