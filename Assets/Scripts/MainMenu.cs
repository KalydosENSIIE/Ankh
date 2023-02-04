using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private int level1Index = 1;
    [SerializeField] private Fader fader;
    private void Start()
    {
        fader.FadeIn();
    }
    public void Play()
    {
        LoadScene(level1Index);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadScene(int sceneIndex)
    {
        fader.TransitionToScene(sceneIndex);
    }
}
