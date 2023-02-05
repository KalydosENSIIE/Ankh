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
    private AbilityHandler abilityHandler;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Controller>();
        playerInput = GetComponent<PlayerInput>();
        abilityHandler = GetComponent<AbilityHandler>();
    }

    void Update()
    {
        controller.Move(moveDirection);
    }

    private void OnMove(InputValue value)
    {
        if (Time.timeScale == 0) return;
        moveDirection = value.Get<Vector2>();
    }

    private void OnLightAttack()
    {
        abilityHandler.TryUseAttack(0);
    }
    private void OnHeavyAttack()
    {
        abilityHandler.TryUseAttack(1);
    }
    private void OnRangedAttack()
    {
        abilityHandler.TryUseAttack(2);
    }
    private void OnAreaAttack()
    {
        abilityHandler.TryUseAttack(3);
    }
    private void OnBlock(InputValue value)
    {
        abilityHandler.Block(value.isPressed);
    }

}
