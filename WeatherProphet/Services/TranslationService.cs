using System;
using System.Collections.Generic;
using System.Globalization;

namespace WeatherProphet.Services
{
    public static class TranslationService
    {
        private static readonly Dictionary<string, string> _weatherPhrases = new()
        {
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
            { "zu", "Ukhumusha kwamanzi emhlabeni ngo" }
        };

        private static readonly Dictionary<string, string> _appTitles = new()
        {
            { "af", "Weer Profeet" },
            { "al", "Parashikues i Motit" },
            { "ar", "نبوءة الطقس" },
            { "az", "Hava Məşhuru" },
            { "bg", "Метеороложки пророк" },
            { "ca", "Profeta del temps" },
            { "cz", "Meteorologický prorok" },
            { "da", "Vejrprofet" },
            { "de", "Wetterprophet" },
            { "el", "Προφήτης καιρού" },
            { "en", "Weather Prophet" },
            { "es", "Pronóstico del Tiempo" },
            { "eu", "Eguraldiaren profeta" },
            { "fa", "پیشگوی آب و هوا" },
            { "fi", "Sääprofeetta" },
            { "fr", "Prophète de la météo" },
            { "gl", "Profeta do tempo" },
            { "he", "הנביא של מזג האוויר" },
            { "hi", "मौसम प्रेमी" },
            { "hr", "Vremenski prorok" },
            { "hu", "Időjós" },
            { "id", "Nabi Cuaca" },
            { "it", "Profeta del Tempo" },
            { "ja", "気象予言者" },
            { "kr", "날씨 예측자" },
            { "la", "Laika Prorok" },
            { "lt", "Orų pranašas" },
            { "mk", "Временски Пророк" },
            { "nl", "Weerprofeet" },
            { "no", "Værprofet" },
            { "pl", "Prorok Pogody" },
            { "pt", "Previsão do Tempo" },
            { "pt_br", "Previsão do Tempo" },
            { "ro", "Prognoza Meteo" },
            { "ru", "Прогноз погоды" },
            { "sk", "Predpoveď počasia" },
            { "sl", "Vremenska napoved" },
            { "sr", "Vremenska prognoza" },
            { "sv", "Väderprognos" },
            { "th", "ผู้พยากรณ์อากาศ" },
            { "tr", "Hava Durumu İhtiyacı" },
            { "ua", "Пророк Погоди" },
            { "vi", "Tiên Tri Thời Tiết" },
            { "zh_cn", "天气预言家" },
            { "zh_tw", "天氣預言家" },
            { "zu", "Inyanga Yezulu" }
        };

        private static readonly Dictionary<string, string> _buttonLabels = new()
        {
            { "af", "Kry Weer" },
            { "al", "Merr Motin" },
            { "ar", "احصل على الطقس" },
            { "az", "Hava almaq" },
            { "bg", "Вземете времето" },
            { "ca", "Obtenir el temps" },
            { "cz", "Získat počasí" },
            { "da", "Få vejret" },
            { "de", "Wetter abrufen" },
            { "el", "Λήψη καιρού" },
            { "en", "Get Weather" },
            { "es", "Obtener Tiempo" },
            { "eu", "Eguraldia lortu" },
            { "fa", "دریافت آب و هوا" },
            { "fi", "Hae sää" },
            { "fr", "Obtenir la météo" },
            { "gl", "Obter tempo" },
            { "he", "קבל מזג אוויר" },
            { "hi", "मौसम प्राप्त करें" },
            { "hr", "Dobiti vrijeme" },
            { "hu", "Időjárás kérés" },
            { "id", "Dapatkan Cuaca" },
            { "it", "Ottieni tempo" },
            { "ja", "天気を取得する" },
            { "kr", "날씨 가져오기" },
            { "la", "Iegūt laiku" },
            { "lt", "Gauti orą" },
            { "mk", "Преземи временска прогноза" },
            { "nl", "Haal het weer op" },
            { "no", "Hent vær" },
            { "pl", "Pobierz pogodę" },
            { "pt", "Obter Tempo" },
            { "pt_br", "Obter Tempo" },
            { "ro", "Obțineți Vremea" },
            { "ru", "Получить погоду" },
            { "sk", "Získať počasie" },
            { "sl", "Pridobi vreme" },
            { "sr", "Preuzmi vreme" },
            { "sv", "Hämta vädret" },
            { "th", "เข้าสู่ระบบสภาพอากาศ" },
            { "tr", "Hava Durumu Al" },
            { "ua", "Отримати погоду" },
            { "vi", "Lấy thời tiết" },
            { "zh_cn", "获取天气" },
            { "zh_tw", "獲取天氣" },
            { "zu", "Thola Izulu" }
        };

        private static readonly Dictionary<string, string> _cityLabels = new()
        {
            { "af", "Stad" }, { "al", "Qyteti" }, { "ar", "المدينة" }, { "az", "Şəhər" },
            { "bg", "Град" }, { "ca", "Ciutat" }, { "cz", "Město" }, { "da", "By" },
            { "de", "Stadt" }, { "el", "Πόλη" }, { "en", "City" }, { "es", "Ciudad" },
            { "eu", "Hiri" }, { "fa", "شهر" }, { "fi", "Kaupunki" }, { "fr", "Ville" },
            { "gl", "Cidade" }, { "he", "עיר" }, { "hi", "शहर" }, { "hr", "Grad" },
            { "hu", "Város" }, { "id", "Kota" }, { "it", "Città" }, { "ja", "都市" },
            { "kr", "도시" }, { "la", "Pilsēta" }, { "lt", "Miestas" }, { "mk", "Град" },
            { "nl", "Stad" }, { "no", "By" }, { "pl", "Miasto" }, { "pt", "Cidade" },
            { "pt_br", "Cidade" }, { "ro", "Oraș" }, { "ru", "Город" }, { "sk", "Mesto" },
            { "sl", "Mesto" }, { "sr", "Grad" }, { "sv", "Stad" }, { "th", "เมือง" },
            { "tr", "Şehir" }, { "ua", "Місто" }, { "vi", "Thành phố" },
            { "zh_cn", "城市" }, { "zh_tw", "城市" }, { "zu", "Isifunda" }
        };

        private static readonly Dictionary<string, string> _forecastLabels = new()
        {
            { "af", "Aantal voorspellings" }, { "al", "Numri i parashikimeve" }, { "ar", "عدد التنبؤات" },
            { "az", "Proqnozların sayı" }, { "bg", "Брой прогнози" }, { "ca", "Nombre de pronòstics" },
            { "cz", "Počet předpovědí" }, { "da", "Antal prognoser" }, { "de", "Anzahl der Vorhersagen" },
            { "el", "Αριθμός προβλέψεων" }, { "en", "Number of forecasts" }, { "es", "Número de pronósticos" },
            { "eu", "Aurreikuspen kopurua" }, { "fa", "تعداد پیش بینی ها" }, { "fi", "Ennusteiden lukumäärä" },
            { "fr", "Nombre de prévisions" }, { "gl", "Número de previsões" }, { "he", "מספר התחזיות" },
            { "hi", "भविष्यवाणी की संख्या" }, { "hr", "Broj prognoza" }, { "hu", "Előrejelzések száma" },
            { "id", "Jumlah ramalan" }, { "it", "Numero di previsioni" }, { "ja", "予報の数" },
            { "kr", "예보 수" }, { "la", "Prognožu skaits" }, { "lt", "Prognozių skaičius" },
            { "mk", "Број на Прогнози" }, { "nl", "Aantal voorspellingen" }, { "no", "Antall prognoser" },
            { "pl", "Liczba prognoz" }, { "pt", "Número de previsões" }, { "pt_br", "Número de previsões" },
            { "ro", "Număr de prognoze" }, { "ru", "Количество прогнозов" }, { "sk", "Počet predpovedí" },
            { "sl", "Število napovedi" }, { "sr", "Broj prognoza" }, { "sv", "Antal prognoser" },
            { "th", "จำนวนการพยากรณ์" }, { "tr", "Tahmin sayısı" }, { "ua", "Кількість прогнозів" },
            { "vi", "Số lượng dự báo" }, { "zh_cn", "预测数量" }, { "zh_tw", "預測數量" },
            { "zu", "Inani Lwezinqumo" }
        };

        private static readonly Dictionary<string, string[]> _dayOfWeekNames = new()
        {
            {"af", new[] {"Sondag", "Maandag", "Dinsdag", "Woensdag", "Donderdag", "Vrydag", "Saterdag"}},
            {"al", new[] {"E Diel", "E Hënë", "E Martë", "E Mërkurë", "E Enjte", "E Premte", "E Shtunë"}},
            {"ar", new[] {"الأحد", "الاثنين", "الثلاثاء", "الأربعاء", "الخميس", "الجمعة", "السبت"}},
            {"az", new[] {"Bazar", "Bazar ertəsi", "Çərşənbə axşamı", "Çərşənbə", "Cümə axşamı", "Cümə", "Şənbə"}},
            {"bg", new[] {"неделя", "понеделник", "вторник", "сряда", "четвъртък", "петък", "събота"}},
            {"ca", new[] {"diumenge", "dilluns", "dimarts", "dimecres", "dijous", "divendres", "dissabte"}},
            {"cz", new[] {"neděle", "pondělí", "úterý", "středa", "čtvrtek", "pátek", "sobota"}},
            {"da", new[] {"søndag", "mandag", "tirsdag", "onsdag", "torsdag", "fredag", "lørdag"}},
            {"de", new[] {"Sonntag", "Montag", "Dienstag", "Mittwoch", "Donnerstag", "Freitag", "Samstag"}},
            {"el", new[] {"Κυριακή", "Δευτέρα", "Τρίτη", "Τετάρτη", "Πέμπτη", "Παρασκευή", "Σάββατο"}},
            {"en", new[] {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"}},
            {"es", new[] {"domingo", "lunes", "martes", "miércoles", "jueves", "viernes", "sábado"}},
            {"eu", new[] {"igandea", "astelehena", "asteartea", "asteazkena", "osteguna", "ostirala", "larunbata"}},
            {"fa", new[] {"یک‌شنبه", "دوشنبه", "سه‌شنبه", "چهارشنبه", "پنج‌شنبه", "جمعه", "شنبه"}},
            {"fi", new[] {"sunnuntai", "maanantai", "tiistai", "keskiviikko", "torstai", "perjantai", "launtai"}},
            {"fr", new[] {"dimanche", "lundi", "mardi", "mercredi", "jeudi", "vendredi", "samedi"}},
            {"gl", new[] {"Domingo", "Luns", "Martes", "Mércores", "Xoves", "Venres", "Sábado"}},
            {"he", new[] {"יום ראשון", "יום שני", "יום שלישי", "יום רביעי", "יום חמישי", "יום שישי", "יום שבת"}},
            {"hi", new[] {"रविवार", "सोमवार", "मंगलवार", "बुधवार", "गुरुवार", "शुक्रवार", "शनिवार"}},
            {"hr", new[] {"Nedjelja", "Ponedjeljak", "Utorak", "Srijeda", "Četvrtak", "Petak", "Subota"}},
            {"hu", new[] {"vasárnap", "hétfő", "kedd", "szerda", "csütörtök", "péntek", "szombat"}},
            {"id", new[] {"Minggu", "Senin", "Selasa", "Rabu", "Kamis", "Jumat", "Sabtu"}},
            {"it", new[] {"Domenica", "Lunedì", "Martedì", "Mercoledì", "Giovedì", "Venerdì", "Sabato"}},
            {"ja", new[] {"日曜日", "月曜日", "火曜日", "水曜日", "木曜日", "金曜日", "土曜日"}},
            {"kr", new[] {"일요일", "월요일", "화요일", "수요일", "목요일", "금요일", "토요일"}},
            {"la", new[] {"Dies Solis", "Dies Lunae", "Dies Martis", "Dies Mercurii", "Dies Iovis", "Dies Veneris", "Dies Saturni"}},
            {"lt", new[] {"Sekmadienis", "Pirmadienis", "Antradienis", "Trečiadienis", "Ketvirtadienis", "Penktadienis", "Šeštadienis"}},
            {"mk", new[] {"Недела", "Понеделник", "Вторник", "Среда", "Четврток", "Петок", "Сабота"}},
            {"no", new[] {"Søndag", "Mandag", "Tirsdag", "Onsdag", "Torsdag", "Fredag", "Lørdag"}},
            {"nl", new[] {"Zondag", "Maandag", "Dinsdag", "Woensdag", "Donderdag", "Vrijdag", "Zaterdag"}},
            {"pl", new[] {"Niedziela", "Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek", "Sobota"}},
            {"pt", new[] {"Domingo", "Segunda-feira", "Terça-feira", "Quarta-feira", "Quinta-feira", "Sexta-feira", "Sábado"}},
            {"pt_br", new[] {"Domingo", "Segunda-feira", "Terça-feira", "Quarta-feira", "Quinta-feira", "Sexta-feira", "Sábado"}},
            {"ro", new[] {"Duminică", "Luni", "Marți", "Miercuri", "Joi", "Vineri", "Sâmbătă"}},
            {"ru", new[] {"Воскресенье", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота"}},
            {"sv", new[] {"Söndag", "Måndag", "Tisdag", "Onsdag", "Torsdag", "Fredag", "Lördag"}},
            {"sk", new[] {"Nedeľa", "Pondelok", "Utorok", "Streda", "Štvrtok", "Piatok", "Sobota"}},
            {"sl", new[] {"Nedelja", "Ponedeljek", "Torek", "Sreda", "Četrtek", "Petek", "Sobota"}},
            {"sr", new[] {"Недеља", "Понедељак", "Уторак", "Среда", "Четвртак", "Петак", "Субота"}},
            {"th", new[] {"อาทิตย์", "จันทร์", "อังคาร", "พุธ", "พฤหัสบดี", "ศุกร์", "เสาร์"}},
            {"tr", new[] {"Pazar", "Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma", "Cumartesi"}},
            {"ua", new[] {"Неділя", "Понеділок", "Вівторок", "Середа", "Четвер", "П'ятниця", "Субота"}},
            {"vi", new[] {"Chủ nhật", "Thứ hai", "Thứ ba", "Thứ tư", "Thứ năm", "Thứ sáu", "Thứ bảy"}},
            {"zh_cn", new[] {"星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"}},
            {"zh_tw", new[] {"星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"}},
            {"zu", new[] {"Sonto", "Mvulo", "Sibili", "Sithathu", "Sine", "Sihlanu", "Mgqibelo"}}
        };

        public static string GetAppTitle(string langCode) =>
            _appTitles.TryGetValue(langCode, out var val) ? val : "Weather Prophet";

        public static string GetButtonLabel(string langCode) =>
            _buttonLabels.TryGetValue(langCode, out var val) ? val : "Get Weather";

        public static string GetCityLabel(string langCode) =>
            _cityLabels.TryGetValue(langCode, out var val) ? val : "City";

        public static string GetWeatherPhrase(string langCode) =>
            _weatherPhrases.TryGetValue(langCode, out var val) ? val : "Current weather for";

        public static string GetForecastLabel(string langCode) =>
            _forecastLabels.TryGetValue(langCode, out var val) ? val : "Number of forecasts";

        public static string GetDayOfWeek(DateTime date, string langCode)
        {
            if (_dayOfWeekNames.TryGetValue(langCode, out string[] days))
            {
                return days[(int)date.DayOfWeek];
            }

            return date.ToString("dddd", new CultureInfo("en-EN"));
        }
    }
}