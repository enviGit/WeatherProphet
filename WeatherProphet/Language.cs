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
        private List<Language> GetAvailableLanguages()
        {
            var languages = new List<Language>();
            
            foreach (var country in new List<string> { "Afrikaans", "Albanian", "Arabic", "Azerbaijani", "Bulgarian", "Catalan", "Czech", "Danish", "German", "Greek", "English",
                "Basque", "Persian (Farsi)", "Finnish", "French", "Galician", "Hebrew", "Hindi", "Croatian", "Hungarian", "Indonesian", "Italian", "Japanese", "Korean", "Latvian",
                "Lithuanian", "Macedonian", "Norwegian", "Dutch", "Polish", "Portuguese", "Português Brasil", "Romanian", "Russian", "Swedish", "Slovak", "Slovenian", "Spanish",
                "Serbian", "Thai", "Turkish", "Ukrainian", "Vietnamese", "Chinese Simplified", "Chinese Traditional", "Zulu" })
                languages.Add(new Language(country, $"/Icons/{country}.png"));
            
            return languages;
        }
    }
}
