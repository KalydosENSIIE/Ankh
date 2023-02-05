using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot{
    public GameObject item;
    public float probability;
}

public class Enemy : MonoBehaviour
{
    private Controller controller;
    protected Transform playerTransform;
    private AbilityHandler abilityHandler;

    private Vector3 target;
    [SerializeField] private float desiredDistanceFromPlayer = 0.5f;
    [SerializeField] private Vector2 randomMoveTimeInterval = new Vector2(1, 3);
    private Camera cam;
    private float minX, maxX;
    float availableSpaceDistanceCheck = 1;
    public enum EnemyState { TargetPlayer, RandomMove, Idle, AlignWithPlayer }
    protected EnemyState state = EnemyState.RandomMove;
    private float timeBeformNextRandomMoveAvailable = 0;
    protected Health health;
    [SerializeField] private List<Loot> loots;
    [SerializeField] private Vector2 yLimits = new Vector2();

    public virtual void Start()
    {
        controller = GetComponent<Controller>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        health = GetComponent<Health>();
        abilityHandler = GetComponent<AbilityHandler>();
        cam = Camera.main;
    }

    public virtual void Update()
    {
        if (health.IsDead()) return;
        if (timeBeformNextRandomMoveAvailable > 0)
            timeBeformNextRandomMoveAvailable -= Time.deltaTime;
        MoveTowardTarget();
    }
    protected void MoveTowardTarget()
    {
        Vector3 dir = (target - transform.position).normalized;
        Collider[] colliders = Physics.OverlapSphere(transform.position + dir * availableSpaceDistanceCheck, 0.1f);
        if (colliders.Length == 0)
            controller.MoveToward(target);
        else
            controller.Move(Vector2.zero);
    }

    protected void StopMoving()
    {
        target = transform.position;
    }

    protected void TargetPlayer(bool fromRight = true)
    {
        target = playerTransform.position + new Vector3((fromRight ? 1: - 1) * desiredDistanceFromPlayer, 0, 0);
    }

    protected void TargetRandom()
    {
        if (timeBeformNextRandomMoveAvailable > 0)
            return;
        timeBeformNextRandomMoveAvailable = Random.Range(randomMoveTimeInterval.x, randomMoveTimeInterval.y);
        GetBounds();
        target.x = Random.Range(minX, maxX);
        target.y = Random.Range(yLimits.x, yLimits.y);
    }

    protected void AlignWithPlayer()
    {
        target.x = transform.position.x;
        target.y = playerTransform.position.y;
        target.z = playerTransform.position.z;
    }

    private void GetBounds()
    {
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        minX = bottomLeft.x;
        maxX = topRight.x;
    }

    protected float DistanceFromPlayer()
    {
        return (transform.position - playerTransform.position).magnitude;
    }

    protected void RotateTowardPlayer()
    {
        if (controller.isFacingRight() && playerTransform.position.x < transform.position.x)
            controller.Flip();
        if (!controller.isFacingRight() && playerTransform.position.x > transform.position.x)
            controller.Flip();
    }

    protected void LightAttack()
    {
        RotateTowardPlayer();
        abilityHandler.TryUseAttack(0, playerTransform.position.x > transform.position.x ? 1 : -1);
    }

    protected void HeavyAttack()
    {
        RotateTowardPlayer();
        abilityHandler.TryUseAttack(1, playerTransform.position.x > transform.position.x ? 1 : -1);
    }

    protected void ThrowProjectile()
    {
        RotateTowardPlayer();
        abilityHandler.TryUseAttack(2, playerTransform.position.x > transform.position.x ? 1 : -1);
    }

    public void Loot()
    {
        float t = 0;
        float r = Random.Range(0, 1);
        foreach (Loot loot in loots)
        {
            t += loot.probability;
            if (t > r)
            {
                Instantiate(loot.item);
                return;
            }
        }
    }
}
