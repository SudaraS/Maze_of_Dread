using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float lookSpeedX = 1f;
    public float lookSpeedY = 1f;
    public float jumpForce = 8f;

    private CharacterController controller;
    private Camera playerCamera;
    private float rotationX = 0f;

    private Vector3 moveDirection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked; // Hide and lock the cursor
        Cursor.visible = false; // Make cursor invisible
    }

    void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            MovePlayer();
            LookAround();
        }
    }

    void MovePlayer()
{
    // Apply gravity
    float moveDirectionY = controller.isGrounded ? -1f : moveDirection.y;

    // Get rotation input from the left and right arrow keys (or A/D)
    float rotationInput = Input.GetAxis("Horizontal");
    float moveInput = Input.GetAxis("Vertical"); // Forward/backward movement (up/down arrow keys or W/S)

    // Rotate the player with left and right arrow keys
    transform.Rotate(Vector3.up * rotationInput * lookSpeedX);

    // Calculate forward movement based on the player's current rotation
    Vector3 move = transform.forward * moveInput;

    // Apply the movement
    controller.Move(move * walkSpeed * Time.deltaTime);

    // Handle jumping
    if (controller.isGrounded && Input.GetButtonDown("Jump"))
    {
        moveDirectionY = jumpForce; // Jumping
    }

    // Apply gravity
    move.y = moveDirectionY;
    controller.Move(move * Time.deltaTime);
}



    void LookAround()
    {
        // Mouse X controls the horizontal rotation of the player (turning)
        float mouseX = Input.GetAxis("Mouse X") * lookSpeedX;
        // Mouse Y controls the vertical rotation of the camera (looking up/down)
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeedY;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Prevent camera from flipping

        // Apply vertical camera rotation (pitch)
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

        // Apply horizontal player rotation (yaw) for looking around
        transform.Rotate(Vector3.up * mouseX);
    }

    // Additional Methods for Locking/Unlocking Cursor (Optional for UI interaction)
    public void UnlockCursorForUI()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;  // Show cursor if it's interacting with the UI
    }

    public void LockCursorForGameplay()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;  // Hide the cursor when locked for gameplay
    }
}
