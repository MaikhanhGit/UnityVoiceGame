using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public string _mainMenuScene = "MainMenu";
    public string _quitScene = "QuitScene";
    public GameObject _gameManager = null;

    public void ToMainMenu()
    {
        if (_mainMenuScene != null)
        {
            SceneManager.LoadScene(_mainMenuScene);
        }
        
    }

    public void BackToGame()
    {
        if(_gameManager != null)
        {
            Debug.Log("Back To Game");
        }
       
    }

    public void OpenQuit()
    {
        if (_quitScene != null)
        {
            SceneManager.LoadScene(_quitScene);
        }
        
    }
}
