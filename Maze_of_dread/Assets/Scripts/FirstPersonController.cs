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
    //private float rotationX = 0f;
    private Vector3 moveDirection;
    private AudioSource footstepAudioSource;
    private AudioSource collectablesAudioSource;

    public ParticleSystem BurstEffect;
    public AudioClip bookSound;
    public AudioClip swordSound;
    public AudioClip potionSound;
    public AudioClip footstepsClip;

    public float interactDistance = 3f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
        footstepAudioSource = GetComponents<AudioSource>()[0];  // Reference to AudioSource
        collectablesAudioSource = GetComponents<AudioSource>()[1];

        footstepAudioSource.clip = footstepsClip;
        footstepAudioSource.loop = true;
    }

    void Update()
    {
        MovePlayer();
        TryCollect();
    }

    void MovePlayer()
{
    // Apply gravity
    float moveDirectionY = controller.isGrounded ? -1f : moveDirection.y;

    // Get rotation input from the left and right arrow keys (or A/D)
    float rotationInput = Input.GetAxis("Horizontal");
    float moveInput = Input.GetAxis("Vertical"); // Forward/backward movement (up/down arrow keys or W/S)

    // Check if there is forward or backward movement
    if (moveInput != 0 && !footstepAudioSource.isPlaying)
    {
        footstepAudioSource.Play(); // Play footstep sound when moving
    }
    else if (moveInput == 0 && footstepAudioSource.isPlaying)  // Stop footstep sound when not moving
    {
        footstepAudioSource.Stop();  // Stop footstep sound if player isn't moving
    }

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

    
    void TryCollect()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance))
            {
                // Check for the "Collectable" tag
                if (hit.collider.CompareTag("Book"))
                {
                    Debug.Log("Book Collected!");
                    collectablesAudioSource.PlayOneShot(bookSound); // Play collection sound
                    Destroy(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("Sword"))
                {
                    Debug.Log("Sword Collected!");
                    collectablesAudioSource.PlayOneShot(swordSound); // Play collection sound
                    Destroy(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("Potion"))
                {
                    Debug.Log("Potion Collected!");
                    collectablesAudioSource.PlayOneShot(potionSound); // Play collection sound
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}
