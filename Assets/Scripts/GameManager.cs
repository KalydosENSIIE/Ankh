using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private PlayerInput playerInput;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Fader fader;
    [SerializeField] private int mainMenuSceneIndex;

    private void Start()
    {
        fader.FadeIn();
    }
    private void OnPause(InputValue value)
    {
        TogglePause();
    }

    public void TogglePause()
    {
        Time.timeScale = 1 - Time.timeScale;
        if (Time.timeScale <= 0)
            pauseMenu.SetActive(true);
        else
            pauseMenu.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        fader.TransitionToScene(mainMenuSceneIndex);
    }
}
