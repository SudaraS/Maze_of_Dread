using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab;  // Laser particle system prefab
    private Transform laserSpawnPoint;               // Where the laser originates
    [SerializeField] private float maxDistance = 100f; // Maximum laser range
    [SerializeField] private LayerMask enemyLayer;   // Assign this to the "EnemyLayer" in the Inspector
    [SerializeField] private float destroyDelay = 0.2f; // Time before auto-destroy (if no collision happens)
    [SerializeField] private AudioClip laserSound; // Reference to the laser sound effect
     private AudioSource audioSource;  

    void Start()
    {
        // Find the laser spawn point
        laserSpawnPoint = GameObject.FindWithTag("Beam").transform;
    }

    void Update()
    {
        // Detect shooting input (Spacebar)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootLaser();
        }
    }

    void ShootLaser()
    {
        RaycastHit hit;

        // Cast a ray from the player's forward direction
        Ray ray = new Ray(laserSpawnPoint.position, laserSpawnPoint.forward);

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            Debug.Log("Hit: " + hit.collider.name + " | Layer: " + hit.collider.gameObject.layer);

            // Check if the hit object is on the enemy layer
            if (((1 << hit.collider.gameObject.layer) & enemyLayer) != 0)
            {
                Debug.Log("Enemy hit: " + hit.collider.name);
                Destroy(hit.collider.gameObject);
            }

            // Spawn laser pointing towards the hit position
            SpawnLaser(hit.point);
        }
        else
        {
            Debug.Log("No hit detected.");
            // If no hit, shoot laser to max distance in forward direction
            SpawnLaser(ray.origin + ray.direction * maxDistance);
        }
        PlayLaserSound();
    }

    void SpawnLaser(Vector3 targetPosition)
    {
        // Instantiate the laser prefab at the spawn point
        GameObject laserInstance = Instantiate(laserPrefab, laserSpawnPoint.position, Quaternion.identity);

        // Align the laser's direction immediately after instantiation
        Vector3 direction = targetPosition - laserSpawnPoint.position;
        laserInstance.transform.rotation = Quaternion.LookRotation(direction);

        // Ensure the particle system plays correctly
        ParticleSystem ps = laserInstance.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            ps.Clear(); // Clear any residual particles
            ps.Play();  // Start the particle system
        }

        // Add a timed self-destruct for the laser instance
        Destroy(laserInstance, destroyDelay);
    }
    void PlayLaserSound()
    {
        // Check if the laser sound is set
        if (laserSound != null && audioSource != null && !audioSource.isPlaying)
        {
            // Play the laser sound effect if it's not already playing
            audioSource.PlayOneShot(laserSound);
        }
    }
}
