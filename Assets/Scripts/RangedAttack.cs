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
        GameObject obj = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        Projectile projectile = obj.GetComponent<Projectile>();
        StrongAttack attack = obj.GetComponent<StrongAttack>();

        if (projectile)
            projectile.Initialize(gameObject, new Vector3(facingRight ? 1 : -1, 0, 0) * projectileSpeed, parameters);
        if (attack)
            attack.Initialize(parameters);
        
        yield return new WaitForSeconds(parameters.endLag);
        finished = true;
    }
}
