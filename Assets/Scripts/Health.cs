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

    private float currentHitStun = 0;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (currentHitStun > 0)
        {
            currentHitStun -= Time.deltaTime;
            if (currentHitStun < 0) currentHitStun = 0;
        }

    }

    public void TakeDamage(int value, float hitStun = 0)
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
            currentHitStun = Mathf.Max(currentHitStun, hitStun);
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

    public bool Stunned()
    {
        return currentHitStun > 0;
    }
}
