using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Slides : MonoBehaviour
{
    private int currentIndex;

    [SerializeField] private Fader fader;
    [SerializeField] private int nextSceneIndex;

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown) Next();
    }

    public void Next()
    {
        if(!fader.transitioning)
        {
            StartCoroutine(Transition());
        }
    }

    private IEnumerator Transition()
    {
        fader.FadeOut();
        yield return new WaitForSeconds(fader.fadeOutTime);
        transform.GetChild(currentIndex).gameObject.SetActive(false);
        currentIndex ++;
        if (currentIndex < transform.childCount)
            transform.GetChild(currentIndex).gameObject.SetActive(true);
        else
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        fader.FadeIn();
    }
}
