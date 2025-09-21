using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{    
    public int _startGameScene = 4;
    public string _mainMenuScene = "MainMenu";
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

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(_mainMenuScene);
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
