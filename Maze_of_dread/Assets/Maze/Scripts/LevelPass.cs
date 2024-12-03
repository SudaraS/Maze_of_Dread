// using System; // For Action
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement; 
// public class LevelPass : MonoBehaviour
// {
//     public static event Action OnCollected;
//     public GameObject levelPassedPanel; // Reference to the UI panel


//     void Update() // Method name should start with a capital 'U'
//     {
//         transform.localRotation = Quaternion.Euler(90f, Time.time * 100f, 0);
//     }

//     void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             OnCollected?.Invoke(); // Fixed the typo
//             ShowLevelPassedPanel();
//             Destroy(gameObject);
//         }
//     }
//     void ShowLevelPassedPanel()
//     {
//         LevelPassedPanel.SetActive(true);
//         Time.timeScale = 0f; // Pause the game
//     }
//     public void ExitToMainMenu()
//     {
//         Time.timeScale = 1f; // Resume the game time
//         SceneManager.LoadScene("MainMenu"); // Load the main menu scene
//     }
// }
using System; // For Action
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // For scene transitions

public class LevelPass : MonoBehaviour
{
    public static event Action OnCollected;

    public GameObject levelPassedPanel; // Reference to the UI panel

    void Update()
    {
        transform.localRotation = Quaternion.Euler(90f, Time.time * 100f, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollected?.Invoke();
            ShowLevelPassedUI();
            Destroy(gameObject);
        }
    }

    void ShowLevelPassedUI()
    {
        levelPassedPanel.SetActive(true); // Show the panel
        Time.timeScale = 0f; // Pause the game
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f; // Resume the game time
        SceneManager.LoadScene("MainMenuScene"); // Load the main menu scene
    }
}
