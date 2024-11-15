using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Demo"); 
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Demo"); 
    }
    public void LoadLevel3()
    {
        SceneManager.LoadScene("Demo"); 
    }

    public void GoBack()
    {
        SceneManager.LoadScene("StartScene");
    }
}
