using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : Attack
{
    public List<Hitbox> hitboxes;
    public Transform effectPosition;

    public override IEnumerator AttackRoutine(LayerMask enemyLayer, bool facingRight) 
    {
        finished = false;
        yield return new WaitForSeconds(parameters.startTime);
        if (audioSource && audioSource.clip != null)
            audioSource.Play();

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
        }
        foreach(Collider collider in hitColliders)
        {
            Health health = collider.GetComponent<Health>();
            if (health) health.Hit(parameters, !facingRight);
        }
        
        if (hitColliders.Count > 0 && parameters.effect)
        {
            Vector3 pos = transform.position;
            if (effectPosition)
            {
                pos = effectPosition.position;
            }
            GameObject effect = Instantiate(parameters.effect, pos, Quaternion.identity);
            if (!facingRight)
                effect.transform.Rotate(0, 180, 0);
            effect.transform.localScale = parameters.effectScale * Vector3.one;
        }
        yield return new WaitForSeconds(parameters.endLag);
        finished = true;
    }
}
