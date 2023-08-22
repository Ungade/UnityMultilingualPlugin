using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MultilingualPlugin
{
    public static class Multilingual
    {
        public static List<AllLanguages> AllLanguagesList => Enum.GetValues(typeof(AllLanguages)).Cast<AllLanguages>().ToList();

        public static AllLanguages CurrentLanguage
        {
            get => MultilingualPluginSettingsProvider.SelectedLanguage; 
            set
            {
                MultilingualPluginSettingsProvider.SelectedLanguage = value;
                OnLanguageChange?.Invoke(value);
            }
        }

        public static Dictionary<AllLanguages, bool> IsActiveLanguages => MultilingualPluginSettingsProvider.IsActiveLanguages;

        public static Dictionary<AllLanguages, int> ActiveLanguagesWithIndexes => MultilingualPluginSettingsProvider.ActiveLanguagesWithIndexes;

        public static List<AllLanguages> ActiveLanguages => ActiveLanguagesWithIndexes.Select(x => x.Key).ToList();

        public static Action<AllLanguages> OnLanguageChange;

        public static string DictionaryToString<k, v>(Dictionary<k, v> dictionary)
        {
            var items = from kvp in dictionary select kvp.Key + ":" + kvp.Value;
            return string.Join(",\n", items);
        }

        public static Dictionary<k, v> StringToDictionary<k, v>(string str)
        {
            Dictionary<k, v> returningDictionary = new Dictionary<k, v>();
            foreach (string pair in str.Split(",\n"))
            {
                returningDictionary.Add((k)TypeDescriptor.GetConverter(typeof(k)).ConvertFromInvariantString(pair.Split(":")[0]),
                    (v)TypeDescriptor.GetConverter(typeof(v)).ConvertFromInvariantString(pair.Split(":")[1]));
            }
            return returningDictionary;
        }
    }

    public enum AllLanguages
    {
        Afrikaans,
        Albanian,
        Amharic,
        Arabic,
        Armenian,
        Assamese,
        Azerbaijani,
        Bangla,
        Basque,
        Belarusian,
        Bosnian,
        Bulgarian,
        Catalan,
        Cherokee,
        Chinese_Simplified,
        Chinese_Traditional,
        Croatian,
        Czech,
        Danish,
        Dari,
        Dutch,
        English,
        Esperanto,
        Estonian,
        Filipino,
        Finnish,
        French,
        Galician,
        Georgian,
        German,
        Greek,
        Gujarati,
        Hausa,
        Hebrew,
        Hindi,
        Hungarian,
        Icelandic,
        Igbo,
        Indonesian,
        Irish,
        Italian,
        Japanese,
        Kannada,
        Kazakh,
        Khmer,
        Kiche,
        Kinyarwanda,
        Konkani,
        Korean,
        Kyrgyz,
        Latvian,
        Lithuanian,
        Luxembourgish,
        Macedonian,
        Malay,
        Malayalam,
        Maltese,
        Maori,
        Marathi,
        Mongolian,
        Nepali,
        Norwegian,
        Odia,
        Persian,
        Polish,
        Portuguese,
        Portuguese_Brazil,
        Punjabi_Gurmukhi,
        Punjabi_Shahmukhi,
        Quechua,
        Romanian,
        Russian,
        Scots,
        Serbian,
        Sindhi,
        Sinhala,
        Slovak,
        Slovenian,
        Sorani,
        Sotho,
        Spanish_Latin_America,
        Spanish_Spain,
        Swahili,
        Swedish,
        Tajik,
        Tamil,
        Tatar,
        Telugu,
        Thai,
        Tigrinya,
        Tswana,
        Turkish,
        Turkmen,
        Ukrainian,
        Urdu,
        Uyghur,
        Uzbek,
        Valencian,
        Vietnamese,
        Welsh,
        Wolof,
        Xhosa,
        Yoruba,
        Zulu,
    }
}