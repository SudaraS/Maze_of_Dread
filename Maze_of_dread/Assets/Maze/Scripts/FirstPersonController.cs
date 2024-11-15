using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private int totalItems = 3;
    private int itemsCollected = 0;
    public  GameObject GameOverScreen;

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


        if (GameOverScreen == null)
        {
            GameOverScreen = GameObject.Find("GameOverPanel");
            GameOverScreen.SetActive(false);
            if (GameOverScreen == null)
            {
                Debug.LogError("GameOverPanel not found! Check the name or ensure it's in the scene.");
            }
        }

        if(GameOverScreen != null){
            GameOverScreen.SetActive(false);
        }
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
                if (hit.collider.CompareTag("Book"))
                {
                    CollectItem(bookSound, hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("Sword"))
                {
                    CollectItem(swordSound, hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("Potion"))
                {
                    CollectItem(potionSound, hit.collider.gameObject);
                }
            }
        }
    }

    void CollectItem(AudioClip clip, GameObject item)
    {
        collectablesAudioSource.PlayOneShot(clip);
        Destroy(item);
        itemsCollected++;

        if (itemsCollected >= totalItems)
        {
            ShowGameOver();
        }
    }

    void ShowGameOver()
    {
        GameOverScreen.SetActive(true);
        Time.timeScale = 0f;  // Pause the game
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;  // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
