using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance{get; private set;}
    private PlayerInput playerInput;
    private Vector2 moveDirection;
    private Controller controller;

<<<<<<< HEAD
    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
=======
>>>>>>> 05113bdb43c2d0e35ef2b3f8e89e4ffad1a96ef8
    void Start()
    {
        controller = GetComponent<Controller>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        controller.Move(moveDirection);
    }

    private void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
        
    }

}
