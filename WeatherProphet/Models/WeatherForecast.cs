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
        public string Language { get; set; }

        public WeatherForecast(DateTime date, string weather, double temperature, string imageUrl, string dayOfWeekName)
        {
            Date = date;
            Weather = weather;
            Temperature = temperature;
            ImageUrl = imageUrl;
            DayOfWeek = dayOfWeekName;
        }
    }
}
