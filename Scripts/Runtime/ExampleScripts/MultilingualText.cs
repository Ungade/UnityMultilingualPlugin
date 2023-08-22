using UnityEngine;
using TMPro;
using MultilingualPlugin;

public class MultilingualText : MultilingualBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private MultilingualObject<string> _text;
    [SerializeField] private MultilingualObject<float> _fontSize;
    [SerializeField] private MultilingualObject<TMP_FontAsset> _font;

    private void Reset()
    {
        if (TryGetComponent(out _label) == false)
        {
            return;
        }
        for (int i = 0; i < Multilingual.AllLanguagesList.Count; i++)
        {
            _text.Values[i] = _label.text;
            _fontSize.Values[i] = _label.fontSize;
            if (_label.font != null)
            {
                _font.Values[i] = _label.font;
            }
        }
    }

    public override void Localization(AllLanguages languages)
    {
        _label.text = _text.Value;
        _label.fontSize = _fontSize.Value;
        _label.font = _font.Value;
    }
}
