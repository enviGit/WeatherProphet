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
using System.Windows.Media.Imaging;

#nullable disable

namespace WeatherProphet
{
    public partial class MainWindow : Window
    {
        private readonly string apiKey;
        private readonly HttpClient httpClient;
        public Language SelectedLanguage { get; set; }
        private string langCode = "en";
        private Dictionary<string, string> weatherPhrases = new Dictionary<string, string> {
            { "af", "Aktuele weer vir" },
            { "al", "Moti aktual për" },
            { "ar", "الطقس الحالي لـ" },
            { "az", "Hazırda hava üçün" },
            { "eu", "Gaurko eguraldia" },
            { "bg", "Текуща прогноза за времето за" },
            { "ca", "El temps actual per a" },
            { "zh_cn", "当前天气为" },
            { "zh_tw", "目前天氣為" },
            { "hr", "Trenutno vrijeme za" },
            { "cz", "Aktuální počasí pro" },
            { "da", "Aktuelt vejr for" },
            { "nl", "Huidig weer voor" },
            { "en", "Current weather for" },
            { "fi", "Nykyinen sää kohteessa" },
            { "fr", "Météo actuelle pour" },
            { "gl", "O tempo actual para" },
            { "de", "Aktuelles Wetter für" },
            { "el", "Τρέχουσες καιρικές συνθήκες για" },
            { "he", "מזג האטמוספירה הנוכחי עבור" },
            { "hi", "वर्तमान मौसम" },
            { "hu", "Jelenlegi időjárás" },
            { "id", "Cuaca saat ini untuk" },
            { "it", "Condizioni meteorologiche attuali per" },
            { "ja", "現在の天気は" },
            { "kr", "현재 날씨" },
            { "la", "Tempestas hodierna pro" },
            { "lt", "Dabartinė oro prognozė" },
            { "mk", "Моментално време за" },
            { "no", "Været nå for" },
            { "fa", "وضعیت آب و هوا در" },
            { "pl", "Aktualna pogoda dla" },
            { "pt", "Tempo atual para" },
            { "pt_br", "Clima atual para"},
            { "ro", "Vremea curentă pentru" },
            { "ru", "Текущая погода для" },
            { "sr", "Trenutno vreme za" },
            { "sk", "Aktuálne počasie pre" },
            { "sl", "Trenutne vremenske razmere za" },
            { "es", "Clima actual para" },
            { "sv", "Aktuellt väder för" },
            { "th", "สภาพในขณะนี้สำหรับ" },
            { "tr", "Şu anki hava durumu" },
            { "ua", "Поточна погода для" },
            { "vi", "Thời tiết hiện tại tại" },
            { "zu", "Ukhumusha kwamanzi emhlabeni ngo" },};

        public MainWindow()
        {
            InitializeComponent();
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
            apiKey = config["OpenWeatherApiKey"];
            httpClient = new HttpClient();
            comboBoxLanguageToDisplay.ItemsSource = WeatherProphet.Language.GetAvailableLanguages();
            DataContext = this;
            comboBoxDaysToShow.Items.Clear();
            comboBoxDaysToShow.Items.Add("1");
            comboBoxDaysToShow.Items.Add("2");
            comboBoxDaysToShow.Items.Add("3");
            comboBoxDaysToShow.Items.Add("4");
        }

        private async Task GetWeather(string city, string language)
        {
            try
            {
                if (!weatherPhrases.ContainsKey(language))
                    language = "en";

                string weatherPhrase = "";
                weatherPhrase = weatherPhrases[language];
                string weatherUrl = $"http://api.openweathermap.org/data/2.5/weather?q={city}&lang={language}&appid={apiKey}&units=metric";
                HttpResponseMessage weatherResponse = await httpClient.GetAsync(weatherUrl);
                weatherResponse.EnsureSuccessStatusCode();
                string weatherResponseBody = await weatherResponse.Content.ReadAsStringAsync();
                dynamic weatherResult = JsonConvert.DeserializeObject(weatherResponseBody);
                string weather = weatherResult.weather[0].description;
                double temperature = Math.Round((double)weatherResult.main.temp, 1);
                string temperatureString = temperature.ToString("0.0", CultureInfo.InvariantCulture);

                if (weather != null && temperature != null)
                    textBlockWeather.Text = $"{weatherPhrase} {city}: {weather}, {temperatureString}°C";
            }
            catch (HttpRequestException ex)
            {
                textBlockWeather.Text = $"Error: {ex.Message}";
                listBoxForecast.Visibility = Visibility.Collapsed;
            }
        }
        private async Task<List<WeatherForecast>> GetWeatherForecasts(string city, int numDays, string language, bool useSelectedValue = true)
        {
            if (useSelectedValue && comboBoxDaysToShow.SelectedItem != null)
            {
                try
                {
                    if (!weatherPhrases.ContainsKey(language))
                        language = "en";

                    string forecastUrl = $"http://api.openweathermap.org/data/2.5/forecast?q={city}&lang={language}&appid={apiKey}&units=metric";
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
                        forecasts.Add(new WeatherForecast(date, weather, temperature, imageUrl, langCode));
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
                await GetWeather(city, langCode);

                if (comboBoxDaysToShow.SelectedItem != null)
                {
                    int numDays = Convert.ToInt32((comboBoxDaysToShow.SelectedItem));
                    var forecasts = await GetWeatherForecasts(city, numDays, langCode);

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
            {
                Resources.MergedDictionaries.Clear();
                ThemeImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Icons/light.png"));
                Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("Themes/LightTheme.xaml", UriKind.Relative) });
            }
            else
            {
                Resources.MergedDictionaries.Clear();
                ThemeImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Icons/dark.png"));
                Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("Themes/DarkTheme.xaml", UriKind.Relative) });
            }
        }

        private async void ComboBoxLanguageToDisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedLanguage = (Language)comboBoxLanguageToDisplay.SelectedItem;

            switch (SelectedLanguage.DisplayName)
            {
                case "Afrikaans":
                    langCode = "af";
                    labelWeatherProphet.Content = "Weer Profeet";
                    labelCity.Content = "Stad";
                    labelForecastsNumber.Content = "Aantal voorspellings";
                    buttonGetWeather.Content = "Kry Weer";
                    break;
                case "Albanian":
                    langCode = "al";
                    labelWeatherProphet.Content = "Parashikues i Motit";
                    labelCity.Content = "Qyteti";
                    labelForecastsNumber.Content = "Numri i parashikimeve";
                    buttonGetWeather.Content = "Merr Motin";
                    break;
                case "Arabic":
                    langCode = "ar";
                    labelWeatherProphet.Content = "نبوءة الطقس";
                    labelCity.Content = "المدينة";
                    labelForecastsNumber.Content = "عدد التنبؤات";
                    buttonGetWeather.Content = "احصل على الطقس";
                    break;
                case "Azerbaijani":
                    langCode = "az";
                    labelWeatherProphet.Content = "Hava Məşhuru";
                    labelCity.Content = "Şəhər";
                    labelForecastsNumber.Content = "Proqnozların sayı";
                    buttonGetWeather.Content = "Hava almaq";
                    break;
                case "Bulgarian":
                    langCode = "bg";
                    labelWeatherProphet.Content = "Метеороложки пророк";
                    labelCity.Content = "Град";
                    labelForecastsNumber.Content = "Брой прогнози";
                    buttonGetWeather.Content = "Вземете времето";
                    break;
                case "Catalan":
                    langCode = "ca";
                    labelWeatherProphet.Content = "Profeta del temps";
                    labelCity.Content = "Ciutat";
                    labelForecastsNumber.Content = "Nombre de pronòstics";
                    buttonGetWeather.Content = "Obtenir el temps";
                    break;
                case "Czech":
                    langCode = "cz";
                    labelWeatherProphet.Content = "Meteorologický prorok";
                    labelCity.Content = "Město";
                    labelForecastsNumber.Content = "Počet předpovědí";
                    buttonGetWeather.Content = "Získat počasí";
                    break;
                case "Danish":
                    langCode = "da";
                    labelWeatherProphet.Content = "Vejrprofet";
                    labelCity.Content = "By";
                    labelForecastsNumber.Content = "Antal prognoser";
                    buttonGetWeather.Content = "Få vejret";
                    break;
                case "German":
                    langCode = "de";
                    labelWeatherProphet.Content = "Wetterprophet";
                    labelCity.Content = "Stadt";
                    labelForecastsNumber.Content = "Anzahl der Vorhersagen";
                    buttonGetWeather.Content = "Wetter abrufen";
                    break;
                case "Greek":
                    langCode = "el";
                    labelWeatherProphet.Content = "Προφήτης καιρού";
                    labelCity.Content = "Πόλη";
                    labelForecastsNumber.Content = "Αριθμός προβλέψεων";
                    buttonGetWeather.Content = "Λήψη καιρού";
                    break;
                case "English":
                    langCode = "en";
                    labelWeatherProphet.Content = "Weather Prophet";
                    labelCity.Content = "City";
                    labelForecastsNumber.Content = "Number of forecasts";
                    buttonGetWeather.Content = "Get Weather";
                    break;
                case "Basque":
                    langCode = "eu";
                    labelWeatherProphet.Content = "Eguraldiaren profeta";
                    labelCity.Content = "Hiri";
                    labelForecastsNumber.Content = "Aurreikuspen kopurua";
                    buttonGetWeather.Content = "Eguraldia lortu";
                    break;
                case "Persian (Farsi)":
                    langCode = "fa";
                    labelWeatherProphet.Content = "پیشگوی آب و هوا";
                    labelCity.Content = "شهر";
                    labelForecastsNumber.Content = "تعداد پیش بینی ها";
                    buttonGetWeather.Content = "دریافت آب و هوا";
                    break;
                case "Finnish":
                    langCode = "fi";
                    labelWeatherProphet.Content = "Sääprofeetta";
                    labelCity.Content = "Kaupunki";
                    labelForecastsNumber.Content = "Ennusteiden lukumäärä";
                    buttonGetWeather.Content = "Hae sää";
                    break;
                case "French":
                    langCode = "fr";
                    labelWeatherProphet.Content = "Prophète de la météo";
                    labelCity.Content = "Ville";
                    labelForecastsNumber.Content = "Nombre de prévisions";
                    buttonGetWeather.Content = "Obtenir la météo";
                    break;
                case "Galician":
                    langCode = "gl";
                    labelWeatherProphet.Content = "Profeta do tempo";
                    labelCity.Content = "Cidade";
                    labelForecastsNumber.Content = "Número de previsões";
                    buttonGetWeather.Content = "Obter tempo";
                    break;
                case "Hebrew":
                    langCode = "he";
                    labelWeatherProphet.Content = "הנביא של מזג האוויר";
                    labelCity.Content = "עיר";
                    labelForecastsNumber.Content = "מספר התחזיות";
                    buttonGetWeather.Content = "קבל מזג אוויר";
                    break;
                case "Hindi":
                    langCode = "hi";
                    labelWeatherProphet.Content = "मौसम प्रेमी";
                    labelCity.Content = "शहर";
                    labelForecastsNumber.Content = "भविष्यवाणी की संख्या";
                    buttonGetWeather.Content = "मौसम प्राप्त करें";
                    break;
                case "Croatian":
                    langCode = "hr";
                    labelWeatherProphet.Content = "Vremenski prorok";
                    labelCity.Content = "Grad";
                    labelForecastsNumber.Content = "Broj prognoza";
                    buttonGetWeather.Content = "Dobiti vrijeme";
                    break;
                case "Hungarian":
                    langCode = "hu";
                    labelWeatherProphet.Content = "Időjós";
                    labelCity.Content = "Város";
                    labelForecastsNumber.Content = "Előrejelzések száma";
                    buttonGetWeather.Content = "Időjárás kérés";
                    break;
                case "Indonesian":
                    langCode = "id";
                    labelWeatherProphet.Content = "Nabi Cuaca";
                    labelCity.Content = "Kota";
                    labelForecastsNumber.Content = "Jumlah ramalan";
                    buttonGetWeather.Content = "Dapatkan Cuaca";
                    break;
                case "Italian":
                    langCode = "it";
                    labelWeatherProphet.Content = "Profeta del Tempo";
                    labelCity.Content = "Città";
                    labelForecastsNumber.Content = "Numero di previsioni";
                    buttonGetWeather.Content = "Ottieni tempo";
                    break;
                case "Japanese":
                    langCode = "ja";
                    labelWeatherProphet.Content = "気象予言者";
                    labelCity.Content = "都市";
                    labelForecastsNumber.Content = "予報の数";
                    buttonGetWeather.Content = "天気を取得する";
                    break;
                case "Korean":
                    langCode = "kr";
                    labelWeatherProphet.Content = "날씨 예측자";
                    labelCity.Content = "도시";
                    labelForecastsNumber.Content = "예보 수";
                    buttonGetWeather.Content = "날씨 가져오기";
                    break;
                case "Latvian":
                    langCode = "la";
                    labelWeatherProphet.Content = "Laika Prorok";
                    labelCity.Content = "Pilsēta";
                    labelForecastsNumber.Content = "Prognožu skaits";
                    buttonGetWeather.Content = "Iegūt laiku";
                    break;
                case "Lithuanian":
                    langCode = "lt";
                    labelWeatherProphet.Content = "Orų pranašas";
                    labelCity.Content = "Miestas";
                    labelForecastsNumber.Content = "Prognozių skaičius";
                    buttonGetWeather.Content = "Gauti orą";
                    break;
                case "Macedonian":
                    langCode = "mk";
                    labelWeatherProphet.Content = "Временски Пророк";
                    labelCity.Content = "Град";
                    labelForecastsNumber.Content = "Број на Прогнози";
                    buttonGetWeather.Content = "Преземи временска прогноза";
                    break;
                case "Norwegian":
                    langCode = "no";
                    labelWeatherProphet.Content = "Værprofet";
                    labelCity.Content = "By";
                    labelForecastsNumber.Content = "Antall prognoser";
                    buttonGetWeather.Content = "Hent vær";
                    break;
                case "Dutch":
                    langCode = "nl";
                    labelWeatherProphet.Content = "Weerprofeet";
                    labelCity.Content = "Stad";
                    labelForecastsNumber.Content = "Aantal voorspellingen";
                    buttonGetWeather.Content = "Haal het weer op";
                    break;
                case "Polish":
                    langCode = "pl";
                    labelWeatherProphet.Content = "Prorok Pogody";
                    labelCity.Content = "Miasto";
                    labelForecastsNumber.Content = "Liczba prognoz";
                    buttonGetWeather.Content = "Pobierz pogodę";
                    break;
                case "Portuguese":
                    langCode = "pt";
                    labelWeatherProphet.Content = "Previsão do Tempo";
                    labelCity.Content = "Cidade";
                    labelForecastsNumber.Content = "Número de previsões";
                    buttonGetWeather.Content = "Obter Tempo";
                    break;
                case "Português Brasil":
                    langCode = "pt_br";
                    labelWeatherProphet.Content = "Previsão do Tempo";
                    labelCity.Content = "Cidade";
                    labelForecastsNumber.Content = "Número de previsões";
                    buttonGetWeather.Content = "Obter Tempo";
                    break;
                case "Romanian":
                    langCode = "ro";
                    labelWeatherProphet.Content = "Prognoza Meteo";
                    labelCity.Content = "Oraș";
                    labelForecastsNumber.Content = "Număr de prognoze";
                    buttonGetWeather.Content = "Obțineți Vremea";
                    break;
                case "Russian":
                    langCode = "ru";
                    labelWeatherProphet.Content = "Прогноз погоды";
                    labelCity.Content = "Город";
                    labelForecastsNumber.Content = "Количество прогнозов";
                    buttonGetWeather.Content = "Получить погоду";
                    break;
                case "Swedish":
                    langCode = "sv";
                    labelWeatherProphet.Content = "Väderprognos";
                    labelCity.Content = "Stad";
                    labelForecastsNumber.Content = "Antal prognoser";
                    buttonGetWeather.Content = "Hämta vädret";
                    break;
                case "Slovak":
                    langCode = "sk";
                    labelWeatherProphet.Content = "Predpoveď počasia";
                    labelCity.Content = "Mesto";
                    labelForecastsNumber.Content = "Počet predpovedí";
                    buttonGetWeather.Content = "Získať počasie";
                    break;
                case "Slovenian":
                    langCode = "sl";
                    labelWeatherProphet.Content = "Vremenska napoved";
                    labelCity.Content = "Mesto";
                    labelForecastsNumber.Content = "Število napovedi";
                    buttonGetWeather.Content = "Pridobi vreme";
                    break;
                case "Spanish":
                    langCode = "es";
                    labelWeatherProphet.Content = "Pronóstico del Tiempo";
                    labelCity.Content = "Ciudad";
                    labelForecastsNumber.Content = "Número de pronósticos";
                    buttonGetWeather.Content = "Obtener Tiempo";
                    break;
                case "Serbian":
                    langCode = "sr";
                    labelWeatherProphet.Content = "Vremenska prognoza";
                    labelCity.Content = "Grad";
                    labelForecastsNumber.Content = "Broj prognoza";
                    buttonGetWeather.Content = "Preuzmi vreme";
                    break;
                case "Thai":
                    langCode = "th";
                    labelWeatherProphet.Content = "ผู้พยากรณ์อากาศ";
                    labelCity.Content = "เมือง";
                    labelForecastsNumber.Content = "จำนวนการพยากรณ์";
                    buttonGetWeather.Content = "เข้าสู่ระบบสภาพอากาศ";
                    break;
                case "Turkish":
                    langCode = "tr";
                    labelWeatherProphet.Content = "Hava Durumu İhtiyacı";
                    labelCity.Content = "Şehir";
                    labelForecastsNumber.Content = "Tahmin sayısı";
                    buttonGetWeather.Content = "Hava Durumu Al";
                    break;
                case "Ukrainian":
                    langCode = "ua";
                    labelWeatherProphet.Content = "Пророк Погоди";
                    labelCity.Content = "Місто";
                    labelForecastsNumber.Content = "Кількість прогнозів";
                    buttonGetWeather.Content = "Отримати погоду";
                    break;
                case "Vietnamese":
                    langCode = "vi";
                    labelWeatherProphet.Content = "Tiên Tri Thời Tiết";
                    labelCity.Content = "Thành phố";
                    labelForecastsNumber.Content = "Số lượng dự báo";
                    buttonGetWeather.Content = "Lấy thời tiết";
                    break;
                case "Chinese Simplified":
                    langCode = "zh_cn";
                    labelWeatherProphet.Content = "天气预言家";
                    labelCity.Content = "城市";
                    labelForecastsNumber.Content = "预测数量";
                    buttonGetWeather.Content = "获取天气";
                    break;
                case "Chinese Traditional":
                    langCode = "zh_tw";
                    labelWeatherProphet.Content = "天氣預言家";
                    labelCity.Content = "城市";
                    labelForecastsNumber.Content = "預測數量";
                    buttonGetWeather.Content = "獲取天氣";
                    break;
                case "Zulu":
                    langCode = "zu";
                    labelWeatherProphet.Content = "Inyanga Yezulu";
                    labelCity.Content = "Isifunda";
                    labelForecastsNumber.Content = "Inani Lwezinqumo";
                    buttonGetWeather.Content = "Thola Izulu";
                    break;
                default:
                    langCode = "en";
                    labelWeatherProphet.Content = "Weather Prophet";
                    labelCity.Content = "City";
                    labelForecastsNumber.Content = "Number of forecasts";
                    buttonGetWeather.Content = "Get Weather";
                    break;
            }

            if (listBoxForecast.ItemsSource != null && textBlockWeather != null)
            {
                string city = textBoxCity.Text;
                int numDays = Convert.ToInt32((comboBoxDaysToShow.SelectedItem));
                await GetWeather(city, langCode);
                var forecasts = await GetWeatherForecasts(city, numDays, langCode);
                listBoxForecast.ItemsSource = forecasts;
            }
        }
    }
}