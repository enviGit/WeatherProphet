using System.Collections.Generic;

namespace WeatherProphet.Models
{
    public class Language
    {
        public string DisplayName { get; set; }
        public string Code { get; set; } // Kod do API i tłumaczeń (np. "en", "pl")
        public string FlagIcon { get; set; }

        public Language(string displayName, string code, string flagIcon)
        {
            DisplayName = displayName;
            Code = code;
            FlagIcon = flagIcon;
        }

        public static List<Language> GetAvailableLanguages()
        {
            var languages = new List<Language>
            {
                new Language("Afrikaans", "af", "/Icons/Afrikaans.png"),
                new Language("Albanian", "al", "/Icons/Albanian.png"),
                new Language("Arabic", "ar", "/Icons/Arabic.png"),
                new Language("Azerbaijani", "az", "/Icons/Azerbaijani.png"),
                new Language("Basque", "eu", "/Icons/Basque.png"),
                new Language("Bulgarian", "bg", "/Icons/Bulgarian.png"),
                new Language("Catalan", "ca", "/Icons/Catalan.png"),
                new Language("Chinese Simplified", "zh_cn", "/Icons/Chinese.png"),
                new Language("Chinese Traditional", "zh_tw", "/Icons/Chinese.png"),
                new Language("Croatian", "hr", "/Icons/Croatian.png"),
                new Language("Czech", "cz", "/Icons/Czech.png"),
                new Language("Danish", "da", "/Icons/Danish.png"),
                new Language("Dutch", "nl", "/Icons/Dutch.png"),
                new Language("English", "en", "/Icons/English.png"),
                new Language("Finnish", "fi", "/Icons/Finnish.png"),
                new Language("French", "fr", "/Icons/French.png"),
                new Language("Galician", "gl", "/Icons/Galician.png"),
                new Language("German", "de", "/Icons/German.png"),
                new Language("Greek", "el", "/Icons/Greek.png"),
                new Language("Hebrew", "he", "/Icons/Hebrew.png"),
                new Language("Hindi", "hi", "/Icons/Hindi.png"),
                new Language("Hungarian", "hu", "/Icons/Hungarian.png"),
                new Language("Indonesian", "id", "/Icons/Indonesian.png"),
                new Language("Italian", "it", "/Icons/Italian.png"),
                new Language("Japanese", "ja", "/Icons/Japanese.png"),
                new Language("Korean", "kr", "/Icons/Korean.png"),
                new Language("Latvian", "la", "/Icons/Latvian.png"),
                new Language("Lithuanian", "lt", "/Icons/Lithuanian.png"),
                new Language("Macedonian", "mk", "/Icons/Macedonian.png"),
                new Language("Norwegian", "no", "/Icons/Norwegian.png"),
                new Language("Persian (Farsi)", "fa", "/Icons/Persian.png"),
                new Language("Polish", "pl", "/Icons/Polish.png"),
                new Language("Portuguese", "pt", "/Icons/Portuguese.png"),
                new Language("Português Brasil", "pt_br", "/Icons/PortuguesBr.png"),
                new Language("Romanian", "ro", "/Icons/Romanian.png"),
                new Language("Russian", "ru", "/Icons/Russian.png"),
                new Language("Serbian", "sr", "/Icons/Serbian.png"),
                new Language("Slovak", "sk", "/Icons/Slovak.png"),
                new Language("Slovenian", "sl", "/Icons/Slovenian.png"),
                new Language("Spanish", "es", "/Icons/Spanish.png"),
                new Language("Swedish", "sv", "/Icons/Swedish.png"),
                new Language("Thai", "th", "/Icons/Thai.png"),
                new Language("Turkish", "tr", "/Icons/Turkish.png"),
                new Language("Ukrainian", "ua", "/Icons/Ukrainian.png"),
                new Language("Vietnamese", "vi", "/Icons/Vietnamese.png"),
                new Language("Zulu", "zu", "/Icons/Afrikaans.png")
            };

            return languages;
        }
    }
}