using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WeatherProphet.Models;
using WeatherProphet.Services;

namespace WeatherProphet.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly WeatherService _weatherService;

        [ObservableProperty]
        private string _cityName;

        [ObservableProperty]
        private WeatherForecast _currentWeather;

        [ObservableProperty]
        private ObservableCollection<WeatherForecast> _forecasts = new();

        [ObservableProperty]
        private ObservableCollection<Language> _availableLanguages;

        [ObservableProperty]
        private Language _selectedLanguage;

        partial void OnSelectedLanguageChanged(Language value)
        {
            UpdateUITexts();
        }

        [ObservableProperty]
        private ObservableCollection<int> _daysOptions = new() { 1, 2, 3, 4, 5 };

        [ObservableProperty]
        private int _selectedDays = 3;

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private string _errorMessage;

        [ObservableProperty]
        private bool _hasError;

        [ObservableProperty] private string _appTitle;
        [ObservableProperty] private string _cityLabel;
        [ObservableProperty] private string _buttonLabel;
        [ObservableProperty] private string _forecastsLabel;

        public MainViewModel()
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = "WeatherProphet.appsettings.json";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    ErrorMessage = "Critical: Configuration resource not found!";
                    HasError = true;
                    return;
                }

                var builder = new ConfigurationBuilder()
                    .AddJsonStream(stream);

                IConfiguration config = builder.Build();

                string apiKey = config["OpenWeatherApiKey"];

                if (string.IsNullOrEmpty(apiKey))
                {
                    ErrorMessage = "API Key missing!";
                    HasError = true;
                }

                _weatherService = new WeatherService(apiKey);
            }

            AvailableLanguages = new ObservableCollection<Language>(Language.GetAvailableLanguages());

            SelectedLanguage = AvailableLanguages.FirstOrDefault(l => l.Code == "en")
                               ?? AvailableLanguages.FirstOrDefault();

            UpdateUITexts();
        }

        [RelayCommand]
        private async Task GetWeather()
        {
            if (string.IsNullOrWhiteSpace(CityName)) return;

            IsLoading = true;
            HasError = false;
            ErrorMessage = "";

            try
            {
                string langCode = SelectedLanguage.Code;

                CurrentWeather = await _weatherService.GetCurrentWeatherAsync(CityName, langCode);

                if (CurrentWeather == null)
                {
                    ErrorMessage = "City not found.";
                    HasError = true;
                    Forecasts.Clear();
                    return;
                }

                var forecastList = await _weatherService.GetForecastAsync(CityName, SelectedDays, langCode);
                Forecasts = new ObservableCollection<WeatherForecast>(forecastList);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error: {ex.Message}";
                HasError = true;
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void UpdateUITexts()
        {
            if (SelectedLanguage == null) return;

            string langCode = SelectedLanguage.Code;

            AppTitle = TranslationService.GetAppTitle(langCode);
            CityLabel = TranslationService.GetCityLabel(langCode);
            ButtonLabel = TranslationService.GetButtonLabel(langCode);
            ForecastsLabel = TranslationService.GetForecastLabel(langCode);
        }
    }
}