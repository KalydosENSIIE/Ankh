using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] float fadeInTime;
    [SerializeField] float fadeOutTime;
    public bool transitioning {get; private set;}
    private Coroutine routine;

    public void FadeOut()
    {
        if (transitioning)
            return;
        if (routine != null)
        {
            StopCoroutine(routine);
        }
        routine = StartCoroutine(FadeOutRoutine(fadeOutTime));
    }
    public void FadeIn()
    {
        if (transitioning)
            return;
        if (routine != null)
        {
            StopCoroutine(routine);
        }
        routine = StartCoroutine(FadeInRoutine(fadeInTime));
    }
    private IEnumerator FadeOutRoutine(float time)
    {
        image.color = Color.clear;
        while (image.color.a < 1)
        {
            Color imageColor = image.color;
            imageColor.a += Time.unscaledDeltaTime / time;
            image.color = imageColor;
            yield return null;
        }
    }

    private IEnumerator FadeInRoutine(float time)
    {
        image.color = Color.black;
        while (image.color.a > 0)
        {
            Color imageColor = image.color;
            imageColor.a -= Time.unscaledDeltaTime / time;
            image.color = imageColor;
            yield return null;
        }
    }

    public void TransitionToScene(int sceneIndex)
    {
        if (transitioning)
            return;
        transitioning = true;
        if (routine != null)
        {
            StopCoroutine(routine);
        }
        routine = StartCoroutine(TransitionRoutine(sceneIndex));
    }
    private IEnumerator TransitionRoutine(int sceneIndex)
    {
        yield return FadeOutRoutine(fadeOutTime);
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneIndex);
    }
}
