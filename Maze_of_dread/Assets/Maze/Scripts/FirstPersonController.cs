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
    private Vector3 moveDirection;
    private AudioSource footstepAudioSource;
    private AudioSource collectablesAudioSource;
    private int totalItems = 3; // Total number of items to collect
    private int itemsCollected = 0; // Counter for collected items
    public GameObject GameOverScreen;

    public ParticleSystem BurstEffect; // Particle effect prefab for collection
    public AudioClip bookSound; // Audio for collecting book
    public AudioClip swordSound; // Audio for collecting sword
    public AudioClip potionSound; // Audio for collecting potion
    public AudioClip footstepsClip; // Footstep audio

    public float interactDistance = 3f; // Distance for interaction

    void Start()
    {
        // Initialize components
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();

        // Get AudioSource components
        AudioSource[] audioSources = GetComponents<AudioSource>();
        if (audioSources.Length >= 2)
        {
            footstepAudioSource = audioSources[0];
            collectablesAudioSource = audioSources[1];
        }
        else
        {
            Debug.LogError("Not enough AudioSource components attached to the GameObject.");
        }

        // Set up footstep audio
        if (footstepAudioSource != null)
        {
            footstepAudioSource.clip = footstepsClip;
            footstepAudioSource.loop = true;
        }

        // Find GameOverPanel in the scene if not assigned
        if (GameOverScreen == null)
        {
            GameOverScreen = GameObject.Find("GameOverPanel");
            if (GameOverScreen == null)
            {
                Debug.LogError("GameOverPanel not found! Ensure it's named correctly in the scene.");
            }
        }

        // Hide the GameOver screen initially
        if (GameOverScreen != null)
        {
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
        if (controller == null) return;

        // Apply gravity
        float moveDirectionY = controller.isGrounded ? -1f : moveDirection.y;

        // Get input for movement and rotation
        float rotationInput = Input.GetAxis("Horizontal");
        float moveInput = Input.GetAxis("Vertical");

        // Handle footsteps
        if (moveInput != 0 && footstepAudioSource != null && !footstepAudioSource.isPlaying)
        {
            footstepAudioSource.Play();
        }
        else if (moveInput == 0 && footstepAudioSource != null && footstepAudioSource.isPlaying)
        {
            footstepAudioSource.Stop();
        }

        // Rotate player
        transform.Rotate(Vector3.up * rotationInput * lookSpeedX);

        // Calculate forward movement
        Vector3 move = transform.forward * moveInput;

        // Apply movement
        controller.Move(move * walkSpeed * Time.deltaTime);

        // Handle jumping
        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            moveDirectionY = jumpForce;
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
                Debug.Log("Hit: " + hit.collider.name); // Debug to see what was hit

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
        if (collectablesAudioSource != null)
        {
            collectablesAudioSource.PlayOneShot(clip);
        }

        if (BurstEffect != null)
        {
            Instantiate(BurstEffect, item.transform.position, Quaternion.identity).Play();
        }

        Destroy(item);
        itemsCollected++;

        Debug.Log($"Collected {itemsCollected}/{totalItems} items.");

        if (itemsCollected >= totalItems)
        {
            ShowGameOver();
        }
    }

    void ShowGameOver()
    {
        if (GameOverScreen != null)
        {
            GameOverScreen.SetActive(true);
        }
        Time.timeScale = 0f; // Pause the game
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
      public void ExitToMainMenu()
    {
        Time.timeScale = 1f; // Resume the game time
        SceneManager.LoadScene("MainMenuScene"); // Load the main menu scene
    }
}
