using System;
using System.Collections.Generic;
using System.Globalization;

namespace WeatherProphet
{
    public class WeatherForecast
    {
        private static readonly Dictionary<string, string[]> DayOfWeekNames = new Dictionary<string, string[]>
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

        public DateTime Date { get; set; }
        public double Temperature { get; set; }
        public string Weather { get; set; }
        public string ImageUrl { get; set; }
        public string DayOfWeek
        {
            get
            {
                if (DayOfWeekNames.TryGetValue(Language, out string[] dayOfWeekNames))
                    return dayOfWeekNames[(int)Date.DayOfWeek];
                else
                    return Date.ToString("dddd", new CultureInfo("en-EN"));
            }
        }
        public string FormattedDate { get { return Date.ToString("dd/MM/yyyy"); } }
        public string Language { get; set; }

        public WeatherForecast(DateTime date, string weather, double temperature, string imageUrl, string language)
        {
            Date = date;
            Weather = weather;
            Temperature = temperature;
            ImageUrl = imageUrl;
            Language = language;
        }
    }
}
