using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : Attack
{
    public List<Hitbox> hitboxes;

    public override IEnumerator AttackRoutine(LayerMask enemyLayer) 
    {
        yield return new WaitForSeconds(parameters.startTime);
        float currentTime = parameters.startTime;
        for (int i = 0; i < hitboxes.Count; i++)
        {
           
            HashSet<Collider> hitColliders = new HashSet<Collider>();
            foreach (Hitbox hitbox in hitboxes)
            {
                Collider[] colliders = Physics.OverlapSphere(hitbox.center.position, hitbox.radius, enemyLayer);
                foreach (Collider collider in colliders)
                {
                    if (!hitColliders.Contains(collider))
                    {
                        hitColliders.Add(collider);
                    }
                }
                foreach(Collider collider in hitColliders)
                {
                    Health health = collider.GetComponent<Health>();
                    if (health) health.Hit(parameters, transform.position.x > collider.transform.position.x);
                }
            }
        }
        if (!useNextAttack)
            yield return new WaitForSeconds(parameters.endLag);
        attackEndEvent.Invoke();
        attackEndEvent = null;
    }
}
