using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "ScriptableObjects/AttackScriptableObject", order = 1)]

public class Hitbox
{
    public Transform center;
    public float radius;
}
public class AttackScriptableObject : ScriptableObject
{

    public int damage = 1;
    public float startTime = 0;
    public float endLag = 0;
    public List<Hitbox> hitboxes;
}
