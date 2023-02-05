using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance{get; private set;}

    [SerializeField]
    private AudioSource attack;
    [SerializeField]
    private AudioSource gameOver;

    void Awake()
    {
        if(!Instance) Instance = this;
        else if(Instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void Attack(AudioClip sound)
    {
        attack.clip = sound;
        attack.Play();
        Debug.Log("play " + sound.name);
    }
    public void GameOver()
    {
        gameOver.Play();
    }
}
