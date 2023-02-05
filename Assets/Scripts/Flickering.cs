using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flickering : MonoBehaviour
{
    [SerializeField] private Color flashColor;
    public float duration = 0.1f;
    [SerializeField] private SpriteRenderer rend;
    [SerializeField] private int flashCount = 1;

    private Color originalColor;

    private Coroutine flashRoutine;


    public void Flash()
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
            rend.material.color = originalColor;
        }
        originalColor = rend.material.color;
        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        float flashTime = duration / (2 * flashCount - 1);
        for (int i = 0; i < flashCount; i++)
        {
            rend.material.color = flashColor;
            yield return new WaitForSeconds(flashTime);
            rend.material.color = originalColor;
            if (i < flashCount - 1)
            {
                yield return new WaitForSeconds(flashTime);
            }
        }
        flashRoutine = null;
    }
}
