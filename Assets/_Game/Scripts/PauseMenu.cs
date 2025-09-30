using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private string _mainMenuScene = "MainMenu";   
    [SerializeField] private Button _pauseBtn = null;
    [SerializeField] private Button _menuBtn = null;
    [SerializeField] private Button _exitBtn = null;
    [SerializeField] private Button _quitBtn = null;
    [SerializeField] private Button _toGameBtn = null;
    [SerializeField] private Button _toPauseBtn = null;
    [SerializeField] private GameObject _pauseMenu = null;
    [SerializeField] private GameObject _pausePanel = null;
    [SerializeField] private GameObject _pauseObj = null;
    [SerializeField] private GameObject _quitPanel = null;
    [SerializeField] private Vector3 _btnSizeChange = new Vector3(1.4f, 1.4f, 1.4f);
    [SerializeField] private AudioClip _sfxEnter = null;
    [SerializeField] private AudioClip _sfxBack = null;
    [SerializeField] private float _sfxVolume = 1.0f;
    [SerializeField] private float _delayTime = 1.0f;
    private Coroutine _coroutine;

    public void PauseGame()
    {
        AnimateButton(_pauseBtn);
        //Add Delay SFX
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _delayTime = _sfxEnter.length;
        _coroutine = StartCoroutine(DelayPause(_sfxEnter, _delayTime));        
    }

    public void BackToGame()
    {        
        AnimateButton(_toGameBtn);
        //Add Delay SFX
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _delayTime = _sfxBack.length;
        _coroutine = StartCoroutine(DelayBackToGame(_sfxBack, _delayTime));

        Time.timeScale = 1;
    }

    public void ToMainMenu()
    {       
        AnimateButton(_menuBtn);
        //Add Delay SFX
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _delayTime = _sfxEnter.length;
        _coroutine = StartCoroutine(DelayLoadScene(_sfxEnter, _mainMenuScene, _delayTime));
        Time.timeScale = 1;
    }

    public void OpenQuitConfirm()
    {
        Time.timeScale = 1;
        AnimateButton(_exitBtn);
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _delayTime = _sfxBack.length;

        _coroutine = StartCoroutine(DelayOpenQuitConfirm(_quitPanel, _pausePanel,
            _exitBtn, _sfxBack, _delayTime));
        
        

    }

    public void BackToPauseMenu()
    {
        Time.timeScale = 1;
        AnimateButton(_toPauseBtn);
        //Add Delay SFX
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _delayTime = _sfxBack.length;

        _coroutine = StartCoroutine(DelayBackToPause(_pausePanel, _quitPanel, _toPauseBtn, _sfxBack, _delayTime));
                
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        AnimateButton(_quitBtn);

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _delayTime = _sfxBack.length;

        _coroutine = StartCoroutine(DelayQuitGame(_sfxBack, _delayTime));
    }

    private IEnumerator DelayQuitGame(AudioClip clip, float delayTime)
    {
        AudioHelper.PlayClip2D(clip, _sfxVolume);

        yield return new WaitForSeconds(delayTime);

        Application.Quit();

    }

    private IEnumerator DelayLoadScene(AudioClip clip, string sceneName, float delayTime)
    {
        if (sceneName == "quit")
        {
            AudioHelper.PlayClip2D(clip, _sfxVolume);

            yield return new WaitForSeconds(delayTime);

            Application.Quit();
        }
        AudioHelper.PlayClip2D(clip, _sfxVolume);

        yield return new WaitForSeconds(delayTime);

        LoadScene(sceneName);
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    

    private IEnumerator DelayPause(AudioClip clip, float delayTime)
    {        
        AudioHelper.PlayClip2D(clip, _sfxVolume);

        yield return new WaitForSeconds(delayTime);

        _pauseMenu.SetActive(true);
        _pauseObj.SetActive(false);
        ResetAnimationButton(_pauseBtn);
        Time.timeScale = 0;
    }

    private IEnumerator DelayBackToGame(AudioClip clip, float delayTime)
    {
        AudioHelper.PlayClip2D(clip, _sfxVolume);

        yield return new WaitForSeconds(delayTime);

        _pauseObj.SetActive(true);
        _pauseMenu.SetActive(false);
        ResetAnimationButton(_toGameBtn);       
        
    }

    private IEnumerator DelayBackToPause(GameObject objToOn, GameObject objToOff, 
        Button btn, AudioClip clip, float delayTime)
    {
        AudioHelper.PlayClip2D(clip, _sfxVolume);

        yield return new WaitForSeconds(delayTime);

        objToOn.SetActive(true);
        objToOff.SetActive(false);
        ResetAnimationButton(btn);
        Time.timeScale = 0;
    }

    private IEnumerator DelayOpenQuitConfirm(GameObject objToOn, GameObject objToOff,
       Button btn, AudioClip clip, float delayTime)
    {
        AudioHelper.PlayClip2D(clip, _sfxVolume);

        yield return new WaitForSeconds(delayTime);

        objToOn.SetActive(true);
        objToOff.SetActive(false);
        ResetAnimationButton(btn);
        Time.timeScale = 0;
    }


    private void AnimateButton(Button btn)
    {
        btn.GetComponent<RectTransform>().localScale = _btnSizeChange;
    }

    private void ResetAnimationButton(Button btn)
    {
        btn.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

}
