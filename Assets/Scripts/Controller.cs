using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private BoxCollider2D bounds;
    [SerializeField] private AnimationCurve knockbackCurve;
    [SerializeField] private Health health;
    public float maxY = 1;
    private bool facingRight = true;
    public float xSpeed = 2;
    public float ySpeed = 1;
    private new Renderer renderer;
    private Coroutine knockbackRoutine;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }
    public void Move(Vector2 moveDirection)
    {
        if (health.Stunned()) return;
        if (moveDirection.x < 0) facingRight = false;
        else if (moveDirection.x > 0) facingRight = true;
        Vector3 previousPosition = transform.position;
        transform.Translate(Vector3.right * moveDirection.x * xSpeed * Time.deltaTime);
        if (renderer.bounds.max.x > bounds.bounds.max.x || renderer.bounds.min.x < bounds.bounds.min.x)
        {
            transform.position = previousPosition;
        }
        if (Global.depthSlope % 180 != 0)
        {
            previousPosition = transform.position;
            transform.Translate(moveDirection.y * ySpeed * Time.deltaTime * (Vector3.up + Vector3.forward / Mathf.Tan(Global.depthSlope / 180 * Mathf.PI)));
            if (renderer.bounds.max.y > bounds.bounds.max.y || renderer.bounds.min.y < bounds.bounds.min.y || renderer.bounds.min.y > maxY)
            {
                transform.position = previousPosition;
            }
        }
    }

    public bool isFacingRight()
    {
        return facingRight;
    }

    public void Knockback(float distance, float duration, bool knockbackedRight)
    {
        if (knockbackRoutine != null)
            StopCoroutine(knockbackRoutine);
        knockbackRoutine = StartCoroutine(KnockbackRoutine(distance, duration, knockbackedRight));
    }

    public IEnumerator KnockbackRoutine(float distance, float duration, bool knockbackedRight)
    {
        float currentTime = 0;
        float currentDistance = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (currentTime > duration) currentTime = 0;
            transform.Translate(Vector3.right * (knockbackedRight ? 1 : -1) * (knockbackCurve.Evaluate(currentTime) - currentDistance));
            currentDistance = knockbackCurve.Evaluate(currentTime);
            yield return 0;
        }
    }
}
