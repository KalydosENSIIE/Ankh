using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHandler : MonoBehaviour
{
    public struct Cooldown
    {
        public float lenght{get; private set;}
        public float startTime;
        public bool finished{get{return Time.time >= startTime + lenght;}}
        public void Start(float lenght)
        {
            this.lenght = lenght;
            startTime = Time.time;
        }
    }

    [SerializeField] private List<Attack> attacks;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Health health;
    private Cooldown[] cooldowns;
    private Coroutine attackRoutine;
    private Attack currentAttack;
    private int currentAttackIndex;
    private bool blocking = false;

    void Start()
    {
        cooldowns = new Cooldown[attacks.Count];
    }

    public void TryUseAttack(int attackIndex)
    {
        if (blocking) return;
        if (health && health.Stunned()) return;
        if (!cooldowns[attackIndex].finished) {Debug.Log("OnCooldown"); return;}
        if (attackRoutine != null && attackIndex == currentAttackIndex) {
            if (currentAttack.nextAttack)
                currentAttack.useNextAttack = true;
            return;
        }
        if (currentAttack && currentAttack.useNextAttack)
        {
            UseAttack(currentAttack.nextAttack);
            cooldowns[currentAttackIndex].Start(currentAttack.parameters.cooldown);
            return;
        }
        UseAttack(attacks[attackIndex]);
        cooldowns[currentAttackIndex].Start(currentAttack.parameters.cooldown);
        currentAttackIndex = attackIndex;
    }

    private void UseAttack(Attack attack)
    {
        currentAttack = attack;
        attackRoutine = StartCoroutine(attack.AttackRoutine(enemyLayer));
        attack.attackEndEvent += () => attackRoutine = null;

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

    /* private IEnumerator AttackRoutine(AttackScriptableObject attack) 
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
    } */

    public void CancelAction()
    {
        if (attackRoutine != null) StopCoroutine(attackRoutine);
        attackRoutine = null;
        currentAttack.useNextAttack = false;
        blocking = false;
    }
}


