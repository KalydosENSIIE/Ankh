using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private UnityEvent damageEvent;
    [SerializeField] private UnityEvent deathEvent;
    [SerializeField] private float invincibilityTime = 0f;

    private int currentHealth;
    private bool alive = true;
    private bool isInvincible = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int value)
    {
        if (!alive || isInvincible) return;
        currentHealth -= value;
        if (currentHealth < 0) currentHealth = 0;
        if (currentHealth == 0)
        {
            alive = false;
            deathEvent.Invoke();
        }
        else if (value > 0)
        {
            StartCoroutine(SetInvincible(invincibilityTime));
            damageEvent.Invoke();
        }
    }
    
    public bool IsDead()
    {
        return !alive;
    }
    public IEnumerator SetInvincible(float duration)
    {
        isInvincible = true;
        yield return new WaitForSeconds(duration);
        isInvincible = false;
    }


}
