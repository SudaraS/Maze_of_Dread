using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab;  // Drag your imported laser prefab here
    private Transform laserSpawnPoint; // Point where the laser will originate

    [SerializeField] private float maxDistance = 100f;  // Maximum laser range

    void Start()
    {
        laserSpawnPoint = GameObject.FindWithTag("Beam").transform;

    }
    void Update()
    {
        // Detect shooting input (e.g., Spacebar or mouse click)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootLaser();
        }
    }

    void ShootLaser()
    {
        RaycastHit hit;
        // Raycast from the camera
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Check if the ray hits something within maxDistance
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            Debug.Log("Hit: " + hit.collider.name);

            // Spawn the laser at the spawn point and make it point towards the hit
            SpawnLaser(hit.point);
        }
        else
        {
            Debug.Log("No hit detected.");
            
            // If no hit, shoot the laser forward to maxDistance
            SpawnLaser(ray.origin + ray.direction * maxDistance);
        }
    }

    void SpawnLaser(Vector3 targetPosition)
    {
        // Instantiate the laser prefab at the spawn point
        GameObject laser = Instantiate(laserPrefab, laserSpawnPoint.position, Quaternion.identity);

        // Rotate the laser to face the target position
        Vector3 direction = targetPosition - laserSpawnPoint.position;
        laser.transform.rotation = Quaternion.LookRotation(direction);

        // Destroy the laser after a short duration
        Destroy(laser, 0.5f);
    }
}

