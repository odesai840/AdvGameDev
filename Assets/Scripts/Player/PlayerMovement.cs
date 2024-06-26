using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private Vector2 input;
    private bool isGrounded;
    public float speed = 22f;
    private float currentSpeed;
    public float gravity = -20f;
    public float jumpHeight = 3f;
    private bool isDashing = true;
    public float dashSpeed = 60f;
    public float dashDuration = 0.5f;
    public float dashTimer = 0f;
    private bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (input != Vector2.zero)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && isMoving && EnergyBar.instance.CanUseDash()) 
        {
            isDashing = true;
            dashTimer = dashDuration;
            EnergyBar.instance.UseDash(25f);
        }
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                isDashing = false;
            }
        }
    }

    // Recieves inputs from InputManager.cs and applies them to character controller
    public void ProcessMovement(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        if (isDashing)
        {
            currentSpeed = dashSpeed;
        }
        else
        {
            currentSpeed = speed;
        }

        controller.Move(currentSpeed * Time.deltaTime * transform.TransformDirection(moveDirection));
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump(bool input)
    {
        if (input && isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }
}
