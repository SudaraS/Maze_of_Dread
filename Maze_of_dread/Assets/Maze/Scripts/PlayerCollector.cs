using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    public float interactDistance = 3f;  // The distance within which the player can interact with the collectible

    private Camera playerCamera;

    public ParticleSystem BurstEffect;
    public AudioClip bookSound;
    public AudioClip swordSound;
    public AudioClip potionSound;
    
    private AudioSource audioSource;


    void Start()
    {
        playerCamera = transform.GetChild(0).GetComponent<Camera>();
        audioSource = GetComponent<AudioSource>();

        // If AudioSource is missing, add one
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)  // Left mouse button click || track pad
        {
            TryCollect();
        }
    }

    void TryCollect()
    {
        // Cast a ray from the camera to the mouse position
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            // Check if the object hit has the "Collectable" tag
            if (hit.collider.CompareTag("Book"))
            {
                Debug.Log("Book Collected!");
                PlayParticleEffect(BurstEffect, hit.point);
                PlaySound(bookSound);
                Destroy(hit.collider.gameObject);
            }
            else if(hit.collider.CompareTag("Sword"))
            {
                Debug.Log("Sword Collected!");
                PlayParticleEffect(BurstEffect, hit.point);
                PlaySound(swordSound);
                Destroy(hit.collider.gameObject);
            }
            else if(hit.collider.CompareTag("Potion"))
            {
                Debug.Log("Potion Collected!");
                PlayParticleEffect(BurstEffect, hit.point);
                PlaySound(potionSound);
                Destroy(hit.collider.gameObject);
            }
        }
    }

    // Method to play particle effect at the collectible's location
    void PlayParticleEffect(ParticleSystem effectPrefab, Vector3 position)
    {
        if (effectPrefab != null)
        {
            ParticleSystem effectInstance = Instantiate(effectPrefab, position, Quaternion.identity);
            effectInstance.Play();
            Destroy(effectInstance.gameObject, effectInstance.main.duration); // Destroy after effect duration
        }
    }
    void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
