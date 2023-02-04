using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : Attack
{
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float projectileSpeed = 15;
    
    public override IEnumerator AttackRoutine(LayerMask enemyLayer) 
    {
        yield return new WaitForSeconds(parameters.startTime);
        Projectile projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation).GetComponent<Projectile>();
        projectile.Initialize(gameObject, projectileSpawnPoint.right * projectileSpeed, parameters);

        if (!useNextAttack)
            yield return new WaitForSeconds(parameters.endLag);
        attackEndEvent.Invoke();
        attackEndEvent = null;
    }
}
