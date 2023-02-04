using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHandler : MonoBehaviour
{
    [SerializeField] private List<AttackScriptableObject> attacks;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Health health;
    private Coroutine attackRoutine;
    private AttackScriptableObject nextAttack;
    private bool useNextAttack;
    private bool blocking = false;
    public void UseAttack(int attackIndex)
    {
        if (blocking) return;
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

    public void Block(bool enabled = true)
    {
        if (blocking || attackRoutine != null || health.Stunned()) return;
        blocking = enabled;
    }


    public bool isBlocking()
    {
        return blocking;
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
                    if (health) health.Hit(attack, transform.position.x > collider.transform.position.x);
                }
            }
        }
        if (!useNextAttack)
            yield return new WaitForSeconds(attack.endLag);
    }

    public void CancelAction()
    {
        if (attackRoutine != null) StopCoroutine(attackRoutine);
        attackRoutine = null;
        useNextAttack = false;
        blocking = false;
    }
}
