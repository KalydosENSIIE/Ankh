using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongAttack : MonoBehaviour
{
    [SerializeField] private float lifeTime = 1;
    [SerializeField] private float fadeOutTime = 1;
    [SerializeField] BoxCollider col;
    float currentTime = 0;
    bool fadeOut = false;
    [SerializeField] SpriteRenderer spriteRenderer;
    private AttackScriptableObject parameters;

    private void Update()
    {
        if (fadeOut) return;
        currentTime += Time.deltaTime;
        if (currentTime > lifeTime)
        {
            fadeOut = true;
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {
        col.enabled = false;
        float a = 1;
        while (a > 0)
        {
            a -= Time.deltaTime;
            if (a < 0) a = 0;
            spriteRenderer.color = new Color(1, 1, 1, a);
            yield return null;
        }
        Destroy(gameObject);
    }

    public void Initialize(AttackScriptableObject parameters)
    {
        this.parameters = parameters;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Health health = other.GetComponent<Health>();
            if (health) health.Hit(parameters, transform.position.x > other.transform.position.x);
        }
    }
}
