using UnityEngine;
using UnityEngine.UI;
using MultilingualPlugin;

public class MultilingualImage : MultilingualBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private MultilingualObject<Sprite> _sprite;

    private void Reset()
    {
        if (TryGetComponent(out _image) == false)
        {
            return;
        }
        if (_image.sprite == null)
        {
            return;
        }
        for (int i = 0; i < Multilingual.AllLanguagesList.Count; i++)
        {
            _sprite.Values[i] = _image.sprite;
        }
    }

    public override void Localization(AllLanguages languages)
    {
        _image.sprite = _sprite.Value;
    }
}
