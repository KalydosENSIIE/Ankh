using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    public AttackScriptableObject parameters;
    public Attack nextAttack;
    public bool useNextAttack{get;set;}
    public delegate void AttackEndEvent();
    public AttackEndEvent attackEndEvent;

    public abstract IEnumerator AttackRoutine(LayerMask enemyLayer);
}
