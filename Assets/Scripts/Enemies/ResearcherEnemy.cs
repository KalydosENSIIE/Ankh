using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearcherEnemy : Enemy
{
    [SerializeField] private float meleeAttackDistance = 1;
    [SerializeField] private float deltaYwithPlayer = 0.1f;
    private float timeBeforeNextAttack;
    private bool tryAttacking = false;
    public override void Start()
    {
        base.Start();
        timeBeforeNextAttack = Random.Range(3, 5);
        state = EnemyState.RandomMove;
    }
    public override void Update()
    {
        base.Update();
        if (health.IsDead()) return;
        if (!playerTransform) return;
        if (state == EnemyState.AlignWithPlayer)
        {
            TargetPlayer();
        }
        if (state == EnemyState.RandomMove)
        {
            TargetRandom();
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
                TargetRandom();
                tryAttacking = false;
                timeBeforeNextAttack = Random.Range(3, 5);
            }
            else {
                state = EnemyState.AlignWithPlayer;
                if (Mathf.Abs(transform.position.y - playerTransform.position.y) < deltaYwithPlayer)
                {
                    ThrowProjectile();
                    TargetRandom();
                    tryAttacking = false;
                    timeBeforeNextAttack = Random.Range(3, 5);
                }
            }
            state = EnemyState.RandomMove;
            
        }
    }
}
