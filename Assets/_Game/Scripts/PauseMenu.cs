using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private string _mainMenuScene = "MainMenu";
    [SerializeField] private string _quitScene = "QuitScene";
    [SerializeField] private Button _pauseBtn = null;
    [SerializeField] private Button _menuBtn = null;
    [SerializeField] private Button _quitBtn = null;
    [SerializeField] private Button _backBtn = null;
    [SerializeField] private GameObject _pauseMenu = null;
    [SerializeField] private GameObject _pauseObj = null;
    [SerializeField] private Vector3 _btnSizeChange = new Vector3(1.4f, 1.4f, 1.4f);
    [SerializeField] private AudioClip _sfxEnter = null;
    [SerializeField] private AudioClip _sfxBack = null;
    [SerializeField] private float _sfxVolume = 1.0f;
    [SerializeField] private float _delayTime = 1.0f;
    private Coroutine _coroutine;

    public void PauseGame()
    {
        AnimatePauseBtn();
        //Add Delay SFX
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _delayTime = _sfxEnter.length;
        _coroutine = StartCoroutine(DelayPause(_sfxEnter, _delayTime));        
    }

    public void UnPauseGame()
    {
        
        ResetAnimatePauseBtn();
        //Add Delay SFX
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _delayTime = _sfxBack.length;
        _coroutine = StartCoroutine(DelayUnpause(_sfxBack, _delayTime));

        Time.timeScale = 1;
    }

    public void ToMainMenu()
    {       
        AnimateMenuBtn();
        //Add Delay SFX
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _delayTime = _sfxEnter.length;
        _coroutine = StartCoroutine(DelayLoadScene(_sfxEnter, _mainMenuScene, _delayTime));
        Time.timeScale = 1;
    }

    public void OpenQuit()
    {
        Time.timeScale = 1;
        AnimateQuitBtn();
        //Add Delay;
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _delayTime = _sfxEnter.length;
        _coroutine = StartCoroutine(DelayLoadScene(_sfxEnter, _quitScene, _delayTime));
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

    private void AnimateQuitBtn()
    {
        _quitBtn.GetComponent<RectTransform>().localScale = _btnSizeChange;

    }

    private IEnumerator DelayPause(AudioClip clip, float delayTime)
    {        
        AudioHelper.PlayClip2D(clip, _sfxVolume);

        yield return new WaitForSeconds(delayTime);

        _pauseMenu.SetActive(true);
        _pauseObj.SetActive(false);


        Time.timeScale = 0;
    }

    private IEnumerator DelayUnpause(AudioClip clip, float delayTime)
    {
        AudioHelper.PlayClip2D(clip, _sfxVolume);

        yield return new WaitForSeconds(delayTime);

        _pauseObj.SetActive(true);
        _pauseMenu.SetActive(false);
        
        
    }
  

    private void AnimateMenuBtn()
    {        
        _menuBtn.GetComponent<RectTransform>().localScale = _btnSizeChange;

    }
    private void AnimatePauseBtn()
    {
        _pauseBtn.GetComponent<RectTransform>().localScale = _btnSizeChange;

    }

    private void ResetAnimatePauseBtn()
    {
        _pauseBtn.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

    
}
