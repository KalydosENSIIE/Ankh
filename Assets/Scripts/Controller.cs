using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private BoxCollider2D bounds;
    private bool facingRight = true;
    public float xSpeed = 2;
    public float ySpeed = 1;
    public float depthSlope = 45;
    private new Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }
    public void Move(Vector2 moveDirection)
    {
        if (moveDirection.x < 0) facingRight = false;
        else if (moveDirection.x > 0) facingRight = true;
        Vector3 previousPosition = transform.position;
        transform.Translate(Vector3.right * moveDirection.x * xSpeed * Time.deltaTime);
        if (renderer.bounds.max.x > bounds.bounds.max.x || renderer.bounds.min.x < bounds.bounds.min.x)
        {
            transform.position = previousPosition;
        }
        if (depthSlope % 180 != 0)
        {
            previousPosition = transform.position;
            transform.Translate(moveDirection.y * ySpeed * Time.deltaTime * (Vector3.up + Vector3.forward / Mathf.Tan(depthSlope / 180 * Mathf.PI)));
            if (renderer.bounds.max.y > bounds.bounds.max.y || renderer.bounds.min.y < bounds.bounds.min.y)
            {
                transform.position = previousPosition;
            }
        }
    }

    public bool isFacingRight()
    {
        return facingRight;
    }
}
