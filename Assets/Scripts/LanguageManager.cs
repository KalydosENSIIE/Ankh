using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public static Language language;
    public Language defaultLanguage;
    public delegate void LanguageChangedEvent();
    public static LanguageChangedEvent languageChangedEvent;

    void Awake()
    {
        SetLanguage(defaultLanguage);
    }

    public void SetLanguage(Language language)
    {
        LanguageManager.language = language;
        languageChangedEvent?.Invoke();
    }
}

[System.Serializable]
public struct DictionaryEntry
{
    public string key;
    [TextArea]
    public string value;
}
