using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static MultilingualPlugin.Multilingual;

namespace MultilingualPlugin
{
    public class MultilingualPluginSettingsProvider : SettingsProvider
    {
        private List<AllLanguages> _allLanguagesList;
        private static AllLanguages? _selectedLanguage;
        private static Dictionary<AllLanguages, bool> _isActiveLanguages;
        private static Dictionary<AllLanguages, int> _activeLanguagesWithIndexes;

        public static AllLanguages SelectedLanguage
        {
            get
            {
                if (Application.isPlaying)
                {
                    if (_selectedLanguage == null)
                    {
                        if (EditorPrefs.HasKey(nameof(SelectedLanguage)) == false)
                        {
                            _selectedLanguage = new AllLanguages();
                        }
                        else
                        {
                            _selectedLanguage = (AllLanguages)Enum.Parse(typeof(AllLanguages), EditorPrefs.GetString(nameof(SelectedLanguage)));
                        }
                    }
                    return (AllLanguages)_selectedLanguage;
                }
                if (EditorPrefs.HasKey(nameof(SelectedLanguage)) == false)
                {
                    return new AllLanguages();
                }
                return (AllLanguages)Enum.Parse(typeof(AllLanguages), EditorPrefs.GetString(nameof(SelectedLanguage)));
            }
            set
            {
                if (Application.isPlaying)
                {
                    _selectedLanguage = value;
                    return;
                }
                EditorPrefs.SetString(nameof(SelectedLanguage), value.ToString());
            }
        }

        public static Dictionary<AllLanguages, bool> IsActiveLanguages
        {
            get
            {
                if (_isActiveLanguages == null)
                {
                    if (EditorPrefs.HasKey(nameof(IsActiveLanguages)))
                    {
                        _isActiveLanguages = StringToDictionary<AllLanguages, bool>(EditorPrefs.GetString(nameof(IsActiveLanguages)));
                    }
                    else
                    {
                        _isActiveLanguages = CreateActiveLanguagesDictionary();
                    }
                }
                return _isActiveLanguages;
            }
            private set => _isActiveLanguages = value;
        }

        public static Dictionary<AllLanguages, int> ActiveLanguagesWithIndexes
        {
            get
            {
                if (_activeLanguagesWithIndexes == null)
                {
                    if (EditorPrefs.HasKey(nameof(ActiveLanguagesWithIndexes)))
                    {
                        _activeLanguagesWithIndexes = StringToDictionary<AllLanguages, int>(EditorPrefs.GetString(nameof(ActiveLanguagesWithIndexes)));
                    }
                    else
                    {
                        _activeLanguagesWithIndexes = new Dictionary<AllLanguages, int>();
                    }
                }
                return _activeLanguagesWithIndexes;
            }
            set => _activeLanguagesWithIndexes = value;
        }

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return new MultilingualPluginSettingsProvider("Project/Multilingual Plugin", SettingsScope.Project);
        }

        public MultilingualPluginSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) : base(path, scopes, keywords) { }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            _allLanguagesList = AllLanguagesList;
            if (EditorPrefs.HasKey(nameof(IsActiveLanguages)))
            {
                IsActiveLanguages = StringToDictionary<AllLanguages, bool>(EditorPrefs.GetString(nameof(IsActiveLanguages)));
            }
            else
            {
                IsActiveLanguages = CreateActiveLanguagesDictionary();
            }
        }

        public override void OnGUI(string searchContext)
        {
            EditorGUI.BeginChangeCheck();
            SelectedLanguage = (AllLanguages)EditorGUILayout.EnumPopup("CurrenLanguage", SelectedLanguage);
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("What languages used");
            foreach (AllLanguages language in _allLanguagesList)
            {
                if (IsActiveLanguages.ContainsKey(language) == false)
                {
                    IsActiveLanguages.Add(language, false);
                }
                IsActiveLanguages[language] = EditorGUILayout.Toggle($"{string.Concat(Enumerable.Repeat(" ", 5))}{language.ToString().Replace('_', ' ')}", IsActiveLanguages[language]);
            }
            if (EditorGUI.EndChangeCheck())
            {
                EditorPrefs.SetString(nameof(IsActiveLanguages), DictionaryToString(IsActiveLanguages));
                ActiveLanguagesWithIndexes = new Dictionary<AllLanguages, int>();
                for (int i = 0; i < IsActiveLanguages.Count; i++)
                {
                    if (IsActiveLanguages.ElementAt(i).Value == true)
                    {
                        ActiveLanguagesWithIndexes.Add(IsActiveLanguages.ElementAt(i).Key , i);
                    }
                }
                EditorPrefs.SetString(nameof(ActiveLanguagesWithIndexes), DictionaryToString(ActiveLanguagesWithIndexes));
            }
        }

        private static Dictionary<AllLanguages, bool> CreateActiveLanguagesDictionary()
        {
            Dictionary<AllLanguages, bool> returningDictionary = new Dictionary<AllLanguages, bool>();
            foreach (AllLanguages language in AllLanguagesList)
            {
                returningDictionary.Add(language, false);
            }
            return returningDictionary;
        }
    }
}