using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Language", menuName = "ScriptableObjects/Language", order = 2)]
public class Language : ScriptableObject
{
    public List<DictionaryEntry> dictionary;

    //Finds the value associated whith key in the dictionary and returns it, or returns the key if not found
    public string Translate(string key)
    {
        foreach(DictionaryEntry entry in dictionary)
        {
            if(entry.key.Equals(key)) return entry.value;
        }
        return key;
    }
}
