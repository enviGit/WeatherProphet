using System.Collections.Generic;

namespace WeatherProphet
{
    public class Language
    {
        public string DisplayName { get; set; }
        public string FlagIcon { get; set; }

        public Language(string displayName, string flagIcon)
        {
            DisplayName = displayName;
            FlagIcon = flagIcon;
        }
        public static List<Language> GetAvailableLanguages()
        {
            var languages = new List<Language>();
            languages.Add(new Language("Afrikaans", "/Icons/Afrikaans.png"));
            languages.Add(new Language("Albanian", "/Icons/Albanian.png"));
            languages.Add(new Language("Arabic", "/Icons/Arabic.png"));
            languages.Add(new Language("Azerbaijani", "/Icons/Azerbaijani.png"));
            languages.Add(new Language("Basque", "/Icons/Basque.png"));
            languages.Add(new Language("Bulgarian", "/Icons/Bulgarian.png"));
            languages.Add(new Language("Catalan", "/Icons/Catalan.png"));
            languages.Add(new Language("Chinese Simplified", "/Icons/Chinese.png"));
            languages.Add(new Language("Chinese Traditional", "/Icons/Chinese.png"));
            languages.Add(new Language("Croatian", "/Icons/Croatian.png"));
            languages.Add(new Language("Czech", "/Icons/Czech.png"));
            languages.Add(new Language("Danish", "/Icons/Danish.png"));
            languages.Add(new Language("Dutch", "/Icons/Dutch.png"));
            languages.Add(new Language("English", "/Icons/English.png"));
            languages.Add(new Language("Finnish", "/Icons/Finnish.png"));
            languages.Add(new Language("French", "/Icons/French.png"));
            languages.Add(new Language("Galician", "/Icons/Galician.png"));
            languages.Add(new Language("German", "/Icons/German.png"));
            languages.Add(new Language("Greek", "/Icons/Greek.png"));
            languages.Add(new Language("Hebrew", "/Icons/Hebrew.png"));
            languages.Add(new Language("Hindi", "/Icons/Hindi.png"));
            languages.Add(new Language("Hungarian", "/Icons/Hungarian.png"));
            languages.Add(new Language("Indonesian", "/Icons/Indonesian.png"));
            languages.Add(new Language("Italian", "/Icons/Italian.png"));
            languages.Add(new Language("Japanese", "/Icons/Japanese.png"));
            languages.Add(new Language("Korean", "/Icons/Korean.png"));
            languages.Add(new Language("Latvian", "/Icons/Latvian.png"));
            languages.Add(new Language("Lithuanian", "/Icons/Lithuanian.png"));
            languages.Add(new Language("Macedonian", "/Icons/Macedonian.png"));
            languages.Add(new Language("Norwegian", "/Icons/Norwegian.png"));
            languages.Add(new Language("Persian (Farsi)", "/Icons/Persian.png"));
            languages.Add(new Language("Polish", "/Icons/Polish.png"));
            languages.Add(new Language("Portuguese", "/Icons/Portuguese.png"));
            languages.Add(new Language("Português Brasil", "/Icons/PortuguesBr.png"));
            languages.Add(new Language("Romanian", "/Icons/Romanian.png"));
            languages.Add(new Language("Russian", "/Icons/Russian.png"));
            languages.Add(new Language("Serbian", "/Icons/Serbian.png"));
            languages.Add(new Language("Slovak", "/Icons/Slovak.png"));
            languages.Add(new Language("Slovenian", "/Icons/Slovenian.png"));
            languages.Add(new Language("Spanish", "/Icons/Spanish.png"));
            languages.Add(new Language("Swedish", "/Icons/Swedish.png"));
            languages.Add(new Language("Thai", "/Icons/Thai.png"));
            languages.Add(new Language("Turkish", "/Icons/Turkish.png"));
            languages.Add(new Language("Ukrainian", "/Icons/Ukrainian.png"));
            languages.Add(new Language("Vietnamese", "/Icons/Vietnamese.png"));
            languages.Add(new Language("Zulu", "/Icons/Afrikaans.png"));

            return languages;
        }
    }
}
