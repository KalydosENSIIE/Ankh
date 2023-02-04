using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : Attack
{
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    
    public override IEnumerator AttackRoutine(LayerMask enemyLayer) 
    {
        yield return new WaitForSeconds(parameters.startTime);
        Instantiate(projectilePrefab, projectileSpawnPoint);
        if (!useNextAttack)
            yield return new WaitForSeconds(parameters.endLag);
        attackEndEvent.Invoke();
        attackEndEvent = null;
    }
}
