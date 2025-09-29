using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{    
    public string _startGameScene = "Sandbox2";
    public string _mainMenuScene = "MainMenu";
    public string _optionsScene = "OptionsScene";
    public string _quitScene = "QuitScene";

    [SerializeField] private Button _optionsBtn = null;
    [SerializeField] private Button _playBtn = null;
    [SerializeField] private Button _quitBtn = null;
    [SerializeField] private Vector3 _btnSizeChange = new Vector3(1.4f, 1.4f, 1.4f);
    [SerializeField] private AudioClip _music = null;
    [SerializeField] private AudioClip _sfxEnter = null;
    [SerializeField] private AudioClip _sfxBack = null;    
    [SerializeField] private float _sfxVolume = 1f;
    [SerializeField] private float _delayTime = .2f;
    private Coroutine _coroutine;

    private void Start()
    {
        if( _music != null)
        {            
            AudioHelper.PlayClip2D(_music, _sfxVolume);
        }        
    }

    public void StartGame()
    {       
        AnimatePlayBtn();
        //Add Delay SFX
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _delayTime = _sfxEnter.length;
        _coroutine = StartCoroutine(DelayLoadScene(_sfxEnter, _startGameScene, _delayTime));                     
    }

    public void OpenMainMenu()
    {                 
        
        //Add Delay;
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }        
        _delayTime = _sfxBack.length;        
        _coroutine = StartCoroutine(DelayLoadScene(_sfxBack, _mainMenuScene, _delayTime));
    }

    public void OpenOptions()
    {    
        AnimateOptionsBtn();
        //Add Delay;
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _delayTime = _sfxEnter.length;
        _coroutine = StartCoroutine(DelayLoadScene(_sfxEnter, _optionsScene, _delayTime));      
    }

    public void OpenQuit()
    {
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
        if(sceneName == "quit")
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

    private void AnimateOptionsBtn()
    {
        _optionsBtn.GetComponent<RectTransform>().localScale = _btnSizeChange;

    }

    private void AnimatePlayBtn()
    {
        _playBtn.GetComponent<RectTransform>().localScale = _btnSizeChange;

    }

    private void AnimateQuitBtn()
    {
        _quitBtn.GetComponent<RectTransform>().localScale = _btnSizeChange;

    }
}
