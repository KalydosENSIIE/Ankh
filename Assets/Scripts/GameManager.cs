using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputActionReference pauseAction;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button backButton;
    [SerializeField] private Fader fader;
    [SerializeField] private int mainMenuSceneIndex;
    [SerializeField] private int nextScene;

    public static GameManager Instance;

    private void Awake()
    {
        Debug.Assert(Instance == null);
        Instance = this;
    }


    private void Start()
    {
        fader.FadeIn();
        pauseAction.action.performed += context => TogglePause();
        pauseAction.action.Enable();
    }

    public void Resume()
    {
        TogglePause();
    }

    public void TogglePause()
    {
        if (!pauseMenu)
            return;
        Time.timeScale = 1 - Time.timeScale;
        if (Time.timeScale <= 0)
            pauseMenu.SetActive(true);
        else
        {
            if (backButton.gameObject.activeSelf)
                backButton.onClick.Invoke();
            pauseMenu.SetActive(false);
        }
    }

    public void ReturnToMainMenu()
    {
        fader.TransitionToScene(mainMenuSceneIndex);
    }

    public void EndGame()
    {
        fader.TransitionToScene(nextScene);
    }

    public void PlayerDead()
    {
        SoundsManager.Instance.GameOver();
        fader.TransitionToScene(SceneManager.GetActiveScene().buildIndex, 2);
    }

    void OnDestroy()
    {
        pauseAction.action.performed -= context => TogglePause();
    }
}
