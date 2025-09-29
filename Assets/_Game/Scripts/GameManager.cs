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
    [SerializeField] private GameObject _boss;
    [SerializeField] private GameObject _player;
    

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
               
        SceneManager.LoadScene(_playSceneToLoad);                
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
