using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UITextTranslator : MonoBehaviour
{
    [Tooltip("The key corresponding to the needed text in the Language")]
    public string key;

    [SerializeField][Tooltip("The TextMeshPro component this will manage the text of")]
    private TextMeshProUGUI textMeshPro;


    // Start is called before the first frame update
    void Start()
    {
        if(!textMeshPro) textMeshPro = GetComponent<TextMeshProUGUI>();
        Translate();
        LanguageManager.languageChangedEvent += OnLanguageChanged;
    }

    private void Translate()
    {
        textMeshPro.text = LanguageManager.language.Translate(key);
    }

    private void OnLanguageChanged()
    {
        Translate();
    }

    void OnDestroy()
    {
        LanguageManager.languageChangedEvent -= OnLanguageChanged;
    }
}
