using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameManager : MonoBehaviour
{
    public GameObject instructionsPanel;
    public GameObject instructionsButton;
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
    public void ShowInstructions()
    {
        // Activate the instructions panel
        if (instructionsPanel != null)
        {
            instructionsPanel.SetActive(true);
        }
        instructionsButton.SetActive(false);
    }

    public void HideInstructions()
    {
        // Deactivate the instructions panel
        if (instructionsPanel != null)
        {
            instructionsPanel.SetActive(false);
        }
        instructionsButton.SetActive(true);
    }
    public void GoBackToMenu()
    {
        // Show the start menu and hide the instructions panel
        //SceneManager.LoadScene("MainMenuScene");
        instructionsPanel.SetActive(false);  // Hide the instructions panel
    }
}
