using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfessorEnemies : Enemy
{
    [SerializeField] private float maxDistanceForAttack = 1;
    private float timeBeforeNextAttack;
    private bool tryAttacking = false;
    public override void Start()
    {
        base.Start();
        timeBeforeNextAttack = Random.Range(3, 5);
    }
    public override void Update()
    {
        base.Update();
        if (health.IsDead()) return;
        if (!playerTransform) return;
        if (state == EnemyState.TargetPlayer)
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
        }
        else
        {
            if (DistanceFromPlayer() < maxDistanceForAttack)
            {
                LightAttack();
                timeBeforeNextAttack = Random.Range(3, 5);
                state = EnemyState.RandomMove;
                TargetRandom();
                tryAttacking = false;
            }
        }
    }
}
