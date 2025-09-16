using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string _playSceneToLoad = "Sandbox";
    [SerializeField] private TouchInputManager _touchInputManager;
    [SerializeField] private GameObject _pauseMenu = null;

    public void StartGame()
    {
        if(_playSceneToLoad != null)
        {
            Debug.Log("Start Game");
            SceneManager.LoadScene(_playSceneToLoad);
        }
        
    }

    private void Update()
    {
        
    }

    public void PauseGame()
    {          
        Debug.Log("Game Paused");   
        if(_pauseMenu != null)
        {
           
        }
        //stop Boss            
        //stop Voice Input       

    }

    public void OpenWin()
    {
        SceneManager.LoadScene("WinScene");
    }
    
    public void ExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();

    }
}
