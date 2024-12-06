using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 0.5f; // Time before auto-destroy (if no collision happens)

    private void Start()
    {
        // Destroy the laser after a short delay to prevent it from lingering forever
        Destroy(gameObject, destroyDelay);
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is on the EnemyLayer
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyLayer"))
        {
            Debug.Log("Enemy hit: " + other.name);

            // Destroy both the laser and the enemy
            Destroy(other.gameObject); // Destroy the enemy
            Destroy(this.gameObject); // Destroy the laser
        }
        else
        {
            Debug.Log("Hit something else: " + other.name);
            Destroy(this.gameObject); // Destroy the laser if it hits something else
        }
    }
}
