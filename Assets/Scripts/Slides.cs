using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slides : MonoBehaviour
{
    private int currentIndex;

    [SerializeField] private Fader fader;

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
        yield return new WaitUntil(()=>!fader.transitioning);
        transform.GetChild(currentIndex).gameObject.SetActive(false);
        currentIndex ++;
        transform.GetChild(currentIndex).gameObject.SetActive(true);
        fader.FadeIn();
    }
}
