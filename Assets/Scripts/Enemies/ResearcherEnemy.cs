using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearcherEnemy : Enemy
{
    [SerializeField] private float meleeAttackDistance = 1;
    private float timeBeforeNextAttack;
    private bool tryAttacking = false;
    public override void Start()
    {
        base.Start();
        timeBeforeNextAttack = Random.Range(7, 10);
    }
    public override void Update()
    {
        base.Update();
        if (health.IsDead()) return;
        if (!playerTransform) return;
        if (state == EnemyState.AlignWithPlayer)
        {
            AlignWithPlayer();
        }
        if (!tryAttacking)
        {
            timeBeforeNextAttack -= Time.deltaTime;
            if (timeBeforeNextAttack < 0)
            {
                timeBeforeNextAttack = 0;
                tryAttacking = true;
                state = EnemyState.TargetPlayer;
            }
            else
            {
                if (DistanceFromPlayer() < meleeAttackDistance){
                    TargetRandom();
                }
            }
        }
        else
        {
            if (DistanceFromPlayer() < meleeAttackDistance)
            {
                LightAttack();
            }
            else {
                ThrowProjectile();
            }
            state = EnemyState.RandomMove;
            TargetRandom();
            Random.Range(7, 10);
        }
    }
}
