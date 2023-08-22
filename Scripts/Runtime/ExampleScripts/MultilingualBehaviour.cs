using UnityEngine;
using MultilingualPlugin;

public abstract class MultilingualBehaviour : MonoBehaviour
{
    private void OnEnable()
    {
        Multilingual.OnLanguageChange += Localization;
    }

    private void OnDisable()
    {
        Multilingual.OnLanguageChange -= Localization;
    }

    private void Start()
    {
        Localization(Multilingual.CurrentLanguage);
    }

    public abstract void Localization(AllLanguages languages);
}
