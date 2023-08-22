using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MultilingualPlugin;
using static MultilingualPlugin.Multilingual;

public class SelectLanguageDropdown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown;

    private void Reset()
    {
        TryGetComponent(out _dropdown);
    }

    private void OnEnable()
    {
        _dropdown.onValueChanged.AddListener(SelectLanguage);
        OnLanguageChange += OnSelectLanguage;
    }

    private void OnDisable()
    {
        _dropdown.onValueChanged.RemoveListener(SelectLanguage);
        OnLanguageChange -= OnSelectLanguage;
    }

    private void Start()
    {
        List<TMP_Dropdown.OptionData> languagesOptionData = new List<TMP_Dropdown.OptionData>();
        foreach (AllLanguages language in ActiveLanguages)
        {
            languagesOptionData.Add(new TMP_Dropdown.OptionData(language.ToString()));
        }
        _dropdown.AddOptions(languagesOptionData);
        SelectLanguage(ActiveLanguages.IndexOf(CurrentLanguage));
    }

    private void SelectLanguage(Int32 languageIndex)
    {
        CurrentLanguage = ActiveLanguages[languageIndex];
    }

    private void OnSelectLanguage(AllLanguages language)
    {
        _dropdown.value = ActiveLanguages.IndexOf(CurrentLanguage);
    }
}
