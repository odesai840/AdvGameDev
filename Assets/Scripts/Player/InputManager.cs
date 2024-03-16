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
    private PlayerShoot shoot;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerInput = new PlayerInput();
        playerControls = playerInput.PlayerControls;
        movement = GetComponent<PlayerMovement>();
        look = GetComponent<PlayerLook>();
        shoot = GetComponent<PlayerShoot>();
        playerControls.Jump.performed += ctx => movement.Jump();
    }

    void Update()
    {
        movement.ProcessMovement(playerControls.Movement.ReadValue<Vector2>());
        look.ProcessLook(playerControls.Look.ReadValue<Vector2>());
        shoot.ProcessShoot(playerControls.Shoot.IsPressed());
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
