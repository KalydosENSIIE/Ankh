using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardEnemy : Enemy
{
    [SerializeField] private float maxDistanceForAttack = 1;
    private bool nextAttackisHeavy = false;
    [SerializeField] private float idleTime = 2;
    private float currentIdleTime = 0;
    public override void Start()
    {
        base.Start();
        state = EnemyState.TargetPlayer;
    }
    public override void Update()
    {
        base.Update();

        if (state == EnemyState.Idle)
        {
            currentIdleTime += Time.deltaTime;
            if (currentIdleTime > idleTime)
            {
                currentIdleTime = 0;
                state = EnemyState.TargetPlayer;
            }
            return;
        }

        if (health.IsDead()) return;
        if (!playerTransform) return;
        TargetPlayer();
        if (DistanceFromPlayer() < maxDistanceForAttack)
        {
            if (nextAttackisHeavy)
            {
                HeavyAttack();
            }
            else
            {
                LightAttack();
            }
            state = EnemyState.Idle;
            nextAttackisHeavy = !nextAttackisHeavy;
        }
        
    }
}
