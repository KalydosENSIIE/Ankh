using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : Attack
{
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float projectileSpeed = 15;
    
    public override IEnumerator AttackRoutine(LayerMask enemyLayer, bool facingRight) 
    {
        finished = false;
        if (audioSource && audioSource.clip != null)
            audioSource.Play();
        yield return new WaitForSeconds(parameters.startTime);
        Projectile projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.Initialize(gameObject, new Vector3(facingRight ? 1 : -1, 0, 0) * projectileSpeed, parameters);

        yield return new WaitForSeconds(parameters.endLag);
        finished = true;
    }
}
