using MultilingualPlugin;
using UnityEngine;

public class MultilingualAudio : MultilingualBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private MultilingualObject<AudioClip> _audioClip;

    private void Reset()
    {
        if (TryGetComponent(out _audioSource) == false)
        {
            return;
        }
        if (_audioSource.clip == null)
        {
            return;
        }
        for (int i = 0; i < Multilingual.AllLanguagesList.Count; i++)
        {
            _audioClip.Values[i] = _audioSource.clip;
        }
    }

    public override void Localization(AllLanguages languages)
    {
        _audioSource.clip = _audioClip.Value;
    }
}
