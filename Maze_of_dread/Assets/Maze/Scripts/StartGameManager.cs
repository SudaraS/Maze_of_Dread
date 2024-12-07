using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Include TextMeshPro namespace

public class StartGameManager : MonoBehaviour
{
    public GameObject instructionsPanel;
    public GameObject instructionsButton;
    public TextMeshProUGUI instructionsText; // Reference to TextMeshPro for instructions text

    // private string spookyStory = 
    //     // "In the shadowy depths of an ancient cursed realm, three forbidden artifacts lie scattered. " +
    //     // "Legend says that collecting all three will awaken an unspeakable horror that will doom even the bravest soul. " +
    //     // "Yet, whispers speak of a loophole: the curse remains dormant if only two of these cursed items are touched. " +
    //     // "However, tampering with even one attracts the attention of vengeful spirits who guard the path to freedom.\n\n" +
    //     "To escape, you must destroy these spirits and find the **blue checkpoint**, a mysterious portal rumored to offer salvation. " +
    //     "But tread carefully—every step, every choice, and every battle draws you deeper into the curse’s grasp. Will you defy the odds, or will you succumb to the darkness?\n";


    public void StartGame()
    {
        // Load the main game scene
        SceneManager.LoadScene("Demo"); 
    }
    void Start()
{
    if (instructionsPanel == null)
    {
        instructionsPanel = GameObject.Find("InstructionsPanel");
        Debug.Log("InstructionsPanel found and assigned dynamically.");
    }
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
        SceneManager.LoadScene("MainMenuScene"); 
    }

    public void ShowInstructions()
{
    // Activate the instructions panel
    if (instructionsPanel != null)
    {
        instructionsPanel.SetActive(true);
    }
    else
    {
        Debug.LogError("InstructionsPanel is null!");
    }

    if (instructionsButton != null)
    {
        instructionsButton.SetActive(false);
    }
    else
    {
        Debug.LogError("InstructionsButton is null!");
    }
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
        // Hide the instructions panel
        instructionsPanel.SetActive(false);
    }
}
