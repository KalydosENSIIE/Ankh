using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private UnityEvent damageEvent;
    [SerializeField] private UnityEvent deathEvent;
    [SerializeField] private UnityEvent healEvent;
    [SerializeField] private AbilityHandler abilityHandler;
    [SerializeField] private Controller controller;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Flickering flickering;

    private int currentHealth;
    private bool alive = true;
    private bool isInvincible = false;
    private Coroutine invincibilityRoutine;

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

    public void Hit(AttackScriptableObject attack, bool fromRight = false)
    {
        if (abilityHandler.isBlocking() && fromRight == controller.isFacingRight() && attack.canBeBlocked)
        {
            controller.Knockback(attack.blockedKnockback, attack.knockbackDuration, !fromRight);
        }
        else
        {
            controller.Knockback(attack.knockback, attack.knockbackDuration, !fromRight);
            TakeDamage(attack.damage, attack.hitStun);
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
            StartCoroutine(DeathCoroutine());
        }
        else if (value > 0)
        {
            currentHitStun = Mathf.Max(currentHitStun, hitStun);
            damageEvent.Invoke();
        }
        if(healthBar) healthBar.value = currentHealth / maxHealth;
    }

    public void Heal(int value)
    {
        if (!alive) return;
        currentHealth += value;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        healthBar.value = currentHealth / maxHealth;
        healEvent.Invoke();
    }
    
    public bool IsDead()
    {
        return !alive;
    }

    public void SetInvincible(float duration)
    {
        if (invincibilityRoutine != null)
        {
            StopCoroutine(invincibilityRoutine);
        }
        invincibilityRoutine = StartCoroutine(InvincibilityRoutine(duration));
    }
    public IEnumerator InvincibilityRoutine(float duration)
    {
        isInvincible = true;
        yield return new WaitForSeconds(duration);
        isInvincible = false;
    }

    public bool Stunned()
    {
        return currentHitStun > 0;
    }

    private IEnumerator DeathCoroutine()
    {
        flickering.Flash();
        yield return new WaitForSeconds(flickering.duration);
        Destroy(gameObject);
    }
}
