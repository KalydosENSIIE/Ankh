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
    [SerializeField] private Animator anim;
    private Cooldown[] cooldowns;
    private Attack currentAttack;
    private int currentAttackIndex = -1;
    private bool blocking = false;
    private bool useNextAttack = false;
    private Controller controller;

    void Start()
    {
        cooldowns = new Cooldown[attacks.Count];
        controller = GetComponent<Controller>();
    }

    public void TryUseAttack(int attackIndex, int dir = 0)
    {
        if (blocking || (currentAttack && useNextAttack)) return;
        if (health && health.Stunned()) return;
        if (attackIndex >= attacks.Count) return;
        if (attackIndex == currentAttackIndex) {
            if (currentAttack.nextAttack) 
            {
                useNextAttack = true;
            }
            return;
        }
        if (currentAttack && !currentAttack.finished) return;
        if (!cooldowns[attackIndex].finished) {return;}
        
        UseAttack(attacks[attackIndex], dir);
        currentAttackIndex = attackIndex;
        cooldowns[currentAttackIndex].Start(currentAttack.parameters.cooldown);
        anim.SetTrigger(attacks[attackIndex].parameters.animationName);
    }

    private void Update()
    {
        if (currentAttack && useNextAttack && currentAttack.finished)
        {
            UseAttack(currentAttack.nextAttack);
            anim.SetTrigger(currentAttack.parameters.animationName);
            useNextAttack = false;
            cooldowns[currentAttackIndex].Start(currentAttack.parameters.cooldown);
            return;
        }
        else
        {
            if (currentAttack && currentAttack.finished)
            {
                currentAttackIndex = -1;
                currentAttack = null;
            }
        }
    }

    private void UseAttack(Attack attack, int dir = 0)
    {
        currentAttack = attack;
        if (dir == -1)
        {
            StartCoroutine(attack.AttackRoutine(enemyLayer, false));
        }
        else if (dir == 1)
        {
            StartCoroutine(attack.AttackRoutine(enemyLayer, true));
        }
        else
            StartCoroutine(attack.AttackRoutine(enemyLayer, !controller.isFacingRight()));

    }

    public void Block(bool enabled = true)
    {
        if (!enabled)
            blocking = false;
        if (blocking || health.Stunned()) return;
        if (currentAttack && !currentAttack.finished) return;
        blocking = enabled;
        anim.SetBool("block", enabled);
    }


    public bool isBlocking()
    {
        return blocking;
    }

    public bool isAttacking()
    {
        if (currentAttack && !currentAttack.finished)
        {
            return true;
        }
        return false;
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
        if (currentAttack)
        {
            useNextAttack = false;
            currentAttack = null;
        }
        blocking = false;
        anim.SetBool("block", false);
    }
}


