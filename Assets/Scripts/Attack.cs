using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    public AttackScriptableObject parameters;
    public Attack nextAttack;
    public bool finished = false;
    public abstract IEnumerator AttackRoutine(LayerMask enemyLayer);
}
