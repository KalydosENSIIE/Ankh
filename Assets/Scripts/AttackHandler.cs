using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    [SerializeField] private List<AttackScriptableObject> attacks;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Health health;
    private Coroutine attackRoutine;
    private AttackScriptableObject nextAttack;
    private bool useNextAttack;
    public void UseAttack(int attackIndex)
    {
        if (health && health.Stunned()) return;
        if (attackRoutine != null) {
            if (nextAttack)
                useNextAttack = true;
            return;
        }
        if (useNextAttack)
        {
            useNextAttack = false;
            StartCoroutine(AttackRoutine(nextAttack));
        }
        attackRoutine = StartCoroutine(AttackRoutine(attacks[attackIndex]));
    }

    private IEnumerator AttackRoutine(AttackScriptableObject attack) 
    {
        nextAttack = attack.nextAttack;
        yield return new WaitForSeconds(attack.startTime);
        float currentTime = attack.startTime;
        for (int i = 0; i < attack.hitboxes.Count; i++)
        {
           
            HashSet<Collider> hitColliders = new HashSet<Collider>();
            foreach (Hitbox hitbox in attack.hitboxes)
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
                    if (health) health.TakeDamage(attack.damage);
                }
            }
        }
        if (!useNextAttack)
            yield return new WaitForSeconds(attack.endLag);
    }

    public void CancelAttack()
    {
        StopCoroutine(attackRoutine);
        attackRoutine = null;
        useNextAttack = false;
    }
}
