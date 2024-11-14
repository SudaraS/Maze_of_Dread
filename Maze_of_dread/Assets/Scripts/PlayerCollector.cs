using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    public float interactDistance = 3f;  // The distance within which the player can interact with the collectible

    private Camera playerCamera;

    void Start()
    {
        playerCamera = transform.GetChild(0).GetComponent<Camera>();
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
            Destroy(hit.collider.gameObject);
        }
        else if(hit.collider.CompareTag("Sword"))
        {
            Debug.Log("Sword Collected!");
            Destroy(hit.collider.gameObject);
        }
        else if(hit.collider.CompareTag("Potion"))
        {
            Debug.Log("Potion Collected!");
            Destroy(hit.collider.gameObject);
        }
        
    }
    }
}
