using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.PlayerControlsActions playerControls;

    private PlayerMovement movement;
    private PlayerLook look;

    void Awake()
    {
        playerInput = new PlayerInput();
        playerControls = playerInput.PlayerControls;
        movement = GetComponent<PlayerMovement>();
        look = GetComponent<PlayerLook>();
        playerControls.Jump.performed += ctx => movement.Jump();
    }

    void FixedUpdate()
    {
        movement.ProcessMovement(playerControls.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        look.ProcessLook(playerControls.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}
