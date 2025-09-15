using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int _playSceneToLoad = 1;

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame == true)
        {
            Debug.Log("Exit Game");
            return;
        }
    }

    private void StartGame()
    {
        Debug.Log("Start Game");
        SceneManager.LoadScene(_playSceneToLoad);
    }

    private void ExitGame()
    {
        Debug.Log("Exit");
        

    }
}
