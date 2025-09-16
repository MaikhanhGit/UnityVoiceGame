using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{    
    public string _startGameScene = "Sandbox";
    public string _optionsScene = "OptionsScene";
    public string _quitScene = "QuitScene";

    private void Start()
    {
        
    }

    public void StartGame()
    {
        Debug.Log("Game Started");
        SceneManager.LoadScene(_startGameScene);
    }

    public void OpenOptions()
    {
        SceneManager.LoadScene(_optionsScene);
    }

    public void OpenQuit()
    {
        SceneManager.LoadScene(_quitScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
