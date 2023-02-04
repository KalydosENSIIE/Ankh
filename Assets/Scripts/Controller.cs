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
    [SerializeField] private Collider col;
    private Coroutine knockbackRoutine;


    public void Move(Vector2 moveDirection)
    {
        if (health.Stunned()) return;
        if (moveDirection.x < 0) facingRight = false;
        else if (moveDirection.x > 0) facingRight = true;
        if ((moveDirection.x > 0 && transform.localScale.x < 0) || (moveDirection.x < 0 && transform.localScale.x > 0))
            Flip();
        Vector3 previousPosition = transform.position;
        transform.Translate(Vector3.right * moveDirection.x * xSpeed * Time.deltaTime);
        if (col.bounds.max.x > bounds.bounds.max.x || col.bounds.min.x < bounds.bounds.min.x)
        {
            transform.position = previousPosition;
        }
        if (Global.depthSlope % 180 != 0)
        {
            previousPosition = transform.position;
            transform.Translate(moveDirection.y * ySpeed * Time.deltaTime * (Vector3.up + Vector3.forward / Mathf.Tan(Global.depthSlope / 180 * Mathf.PI)));
            if (col.bounds.max.y > bounds.bounds.max.y || col.bounds.min.y < bounds.bounds.min.y || col.bounds.min.y > maxY) {
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
    private void Flip()
    {
        Vector3 scale = transform.localScale;
        transform.localScale = new Vector3(scale.x * -1, scale.y, scale.z);
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
