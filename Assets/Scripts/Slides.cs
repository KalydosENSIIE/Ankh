using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Slides : MonoBehaviour
{
    public List<SlideTransition> transitions;
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
        if(currentIndex < transitions.Count)
        {
            if(transitions[currentIndex].fade)
            {
                fader.FadeOut();
                yield return new WaitForSeconds(fader.fadeOutTime);
            }
            foreach(GameObject obj in transitions[currentIndex].disable)
            {
                obj.SetActive(false);
            }
            foreach(GameObject obj in transitions[currentIndex].enable)
            {
                obj.SetActive(true);
            }
            if(transitions[currentIndex].fade)
            {
                fader.FadeIn();
            }
        }
        else
        {
            fader.TransitionToScene(nextSceneIndex);
        }
        currentIndex ++;
    }
}

[System.Serializable]
public struct SlideTransition
{
    public bool fade;
    public List<GameObject> disable;
    public List<GameObject> enable;
}
