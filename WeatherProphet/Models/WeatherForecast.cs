using System;
using System.Collections.Generic;
using System.Globalization;

namespace WeatherProphet.Models
{
    public class WeatherForecast
    {
        
        public DateTime Date { get; set; }
        public double Temperature { get; set; }
        public string Weather { get; set; }
        public string ImageUrl { get; set; }
        public string DayOfWeek { get; set; }
        public string FormattedDate { get { return Date.ToString("dd/MM/yyyy"); } }
        public double FeelsLike { get; set; }
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }
        public int Pressure { get; set; }

        public WeatherForecast(DateTime date, string weather, double temperature, string imageUrl, string dayOfWeekName,
                               double feelsLike = 0, int humidity = 0, double windSpeed = 0, int pressure = 0)
        {
            Date = date;
            Weather = weather;
            Temperature = temperature;
            ImageUrl = imageUrl;
            DayOfWeek = dayOfWeekName;

            FeelsLike = feelsLike;
            Humidity = humidity;
            WindSpeed = windSpeed;
            Pressure = pressure;
        }
    }
}
