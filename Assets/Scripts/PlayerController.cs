using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance{get; private set;}
    private PlayerInput playerInput;
    public BoxCollider2D bounds;
    public float xSpeed = 2;
    public float ySpeed = 1;
    public float depthSlope = 45;
    private Vector2 moveDirection;
    private new Renderer renderer;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        Move();
    }

    private void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
        
    }


    //Move according to moveDirection
    private void Move()
    {
        Vector3 previousPosition = transform.position;
        transform.Translate(Vector3.right * moveDirection.x * xSpeed * Time.deltaTime);
        if(renderer.bounds.max.x > bounds.bounds.max.x || renderer.bounds.min.x < bounds.bounds.min.x)
        {
            transform.position = previousPosition;
        }
        if(depthSlope % 180 != 0)
        {
            previousPosition = transform.position;
            transform.Translate(moveDirection.y * ySpeed * Time.deltaTime * (Vector3.up + Vector3.forward / Mathf.Tan(depthSlope/180*Mathf.PI)));
            if(renderer.bounds.max.y > bounds.bounds.max.y || renderer.bounds.min.y < bounds.bounds.min.y)
            {
                transform.position = previousPosition;
            }
        }
        
    }
}
