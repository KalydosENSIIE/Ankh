using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardEnemy : Enemy
{
    [SerializeField] private float maxDistanceForAttack = 1;
    private bool nextAttackisHeavy = false;
    public override void Start()
    {
        base.Start();
        state = EnemyState.TargetPlayer;
    }
    public override void Update()
    {
        base.Update();
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
            nextAttackisHeavy = !nextAttackisHeavy;
        }
        
    }
}
