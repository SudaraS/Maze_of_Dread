using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameManager : MonoBehaviour
{
    public void StartGame()
    {
        // Load the main game scene
        SceneManager.LoadScene("Demo"); // Replace with your actual game scene name
    }

    public void QuitGame()
    {
        // Quit the game
        Debug.Log("Quit Game"); // This works in the editor
        Application.Quit();    // This works in a built game
    }
    public void LoadMainMenu()
    {
        // Load the Main Menu scene
        SceneManager.LoadScene("MainMenuScene"); // Replace "MainMenuScene" with the name of your Main Menu scene
    }
}
