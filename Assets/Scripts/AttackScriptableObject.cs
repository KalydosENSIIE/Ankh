using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Hitbox
{
    public Transform center;
    public float radius;
}

[CreateAssetMenu(fileName = "Attack", menuName = "ScriptableObjects/AttackScriptableObject", order = 1)]
public class AttackScriptableObject : ScriptableObject
{

    public int damage = 1;
    public float hitStun;
    public float startTime = 0;
    public float endLag = 0;
    public float cooldown = 0;
    public bool canBeBlocked = true;
    public float knockback = 1;
    public float knockbackDuration = 0.5f;
    public float blockedKnockback = 0.1f;
    public string animationName = "";
}
