using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private Vector2 moveDirection;
    private Controller controller;

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
