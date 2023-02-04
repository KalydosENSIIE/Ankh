using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : Collectable
{
    [SerializeField] private int healAmount;
    public override void Effect(GameObject gameObject)
    {
        Health health = gameObject.GetComponent<Health>();
        if (health) health.Heal(healAmount);
    }
}
