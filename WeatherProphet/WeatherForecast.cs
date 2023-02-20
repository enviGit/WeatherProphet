using System;

namespace WeatherProphet
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }
        public double Temperature { get; set; }
        public string Weather { get; set; }
        public string ImageUrl { get; set; }
        public string DayOfWeek { get { return Date.ToString("dddd"); } }
        public string FormattedDate { get { return Date.ToString("dd/MM/yyyy HH:mm"); } }

        public WeatherForecast(DateTime date, string weather, double temperature, string imageUrl)
        {
            Date = date;
            Weather = weather;
            Temperature = temperature;
            ImageUrl = imageUrl;
        }
    }
}
