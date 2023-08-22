# UnityMultilingualPlugin
>**Warning** <br />
> The plugin is still in beta. If you find a bug or have any suggestions, write to [Issues](https://github.com/Text1258/UnityMultilingualPlugin/issues).
## Instalation
1. [Install Git according to your engine version, if you haven't already.](https://docs.unity3d.com/2022.3/Documentation/Manual/upm-ui-giturl.html)
2. Go to `Window/Package Manager`.
3. Click on `+`.
4. Select `Add Package from git URL` in the menu that appears.
5. Enter https://github.com/Text1258/UnityMultilingualPlugin.git and click `Add`.
## Examples
To open a scene with an example of using the plugin, go to the Unity editor in `Packages/Unity Multilingual Plugin/MultilingualExample/MultilingualExample.unitypackage`.
## Plugin Settings
To configure the plugin, go to `Edit/Project Settings/Multilingual Plugin` in the Unity editor. 

Then there is a popup with a default language selection.

And below is a list with languages in which you can mark the languages into which your game will be localized.
>**Warning** <br />
> Make sure that the language you selected is in the active list.
## Using
To use the plugin in your script, enter the following at the beginning:
```C# 
using namespace MultilingualPlugin;
 ```
To localize any data type you want, use the [MultilingualObject\<T>](Scripts/Runtime/MultilingualObject.cs) class, where T is the type you want to localize. To access the value of the currently selected language, refer to MultilingualObject\<T>\.Value. Example:
```C# 
using UnityEngine;
using MultilingualPlugin;

public class ImageLocalization : MonoBehaviour
{
    [SerializeField] private MultilingualObject<string> _phrase;

    private void Start()
    {
        Debug.Log(_phrase.Value);
    }
}
 ```
## Multilingual
[Multilingual](Scripts/Runtime/Multilingual.cs) is a class from which you can get information from a plugin.
> **Note** <br />
> If your script often uses the Multilingual class, then for convenience, you can write at the beginning:
> ```C# 
> using static MultilingualPlugin.Multilingual;
> ```
### AllLanguagesList
List of all languages in the plugin in enum format.
### IsActiveLanguages
Dictionary<AllLanguages, bool>, which has information about whether each language of the plugin is active from the settings.
### ActiveLanguagesWithIndexes
Dictionary<AllLanguages, int> of all active languages from settings and their indexes among the list of all languages.
### ActiveLanguages
List<AllLanguages> of active languages.
### CurrentLanguageIndexInAlllanguages
Index of the current language in the list of all languages.
### CurrentLanguageIndexInUsingLanguages
Index of the current language in the list of selected languages
### CurrentLanguage
CurrentLanguage returns the current language. You can also set this value yourself.
### OnLanguageChange
OnLanguageChange is an event that you can subscribe to if you want to do something when the language changes. Example:
```C# 
using UnityEngine;
using UnityEngine.UI;
using MultilingualPlugin;

public class ImageLocalization : MonoBehaviour
{

    [SerializeField] private Image _image;
    [SerializeField] private MultilingualObject<Sprite> _sprite;

    private void Start()
    {
        LocalizationImage();
    }

    private void OnEnable()
    {
        Multilingual.OnLanguageChange += LocalizationImage;
    }

    private void OnDisable()
    {
        Multilingual.OnLanguageChange -= LocalizationImage;
    }

    public void LocalizationImage()
    {
        if (_image != null)
        {
            _image.sprite = _sprite.Value;
        }
    }
}
 ```
## AllLanguages
AllLanguages is an enum listing all the languages in the plugin.
## Ready-made examples
The plugin contains examples for localization of text, images, audio and for a script for a drop-down list with a choice of language:
- [MultilingualText](Scripts/Runtime/ExampleScripts/MultilingualText.cs)
- [MultilingualImage](Scripts/Runtime/ExampleScripts/MultilingualImage.cs)
- [MultilingualAudio](Scripts/Runtime/ExampleScripts/MultilingualAudio.cs)
- [SelectLanguageDropdown](Scripts/Runtime/ExampleScripts/UI/SelectLanguageDropdown.cs)
### MultilingualBehaviour
You can also write your own localization script by inheriting it from [MultilingualBehaviour](Scripts/Runtime/ExampleScripts/MultilingualBehaviour.cs). Just redefine the Localization method and specify in it how your object will be localized.