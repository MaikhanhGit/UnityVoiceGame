using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _playSceneToLoad = 4;
    [SerializeField] private GameObject _audioLoudnessDetector;
    private bool _isMicAvailable = false;

    private void Awake()
    {
        _isMicAvailable = _audioLoudnessDetector.GetComponent<AudioLoudnessDetector>().GetMicInput();

        if(_isMicAvailable == true)
        {
            _audioLoudnessDetector.GetComponent<AudioLoudnessDetector>().GetMicPermission();
        }
        
    }

    public void StartGame()
    { // TODO: add a delay
        Debug.Log("Start Game");
        SceneManager.LoadScene(_playSceneToLoad);                
    }   

    public void PauseGame()
    {          
        Debug.Log("Game Paused");           
        
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
