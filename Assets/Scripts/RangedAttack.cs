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
        finished = false;
        SoundsManager.Instance.Attack(parameters.sound);
        yield return new WaitForSeconds(parameters.startTime);
        Projectile projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation).GetComponent<Projectile>();
        projectile.Initialize(gameObject, projectileSpawnPoint.right * projectileSpeed, parameters);

        yield return new WaitForSeconds(parameters.endLag);
        finished = true;
    }
}
