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
    [SerializeField] private Button _onBtn = null;
    [SerializeField] private Button _offBtn = null;
    [SerializeField] private Button _backBtn = null;
    [SerializeField] private Button _backToMainBtn = null;
    [SerializeField] private GameObject _optionsMenuCvs = null;
    [SerializeField] private GameObject _quitMenuCvs = null;
    [SerializeField] private GameObject _mainMenuCvs = null;
    [SerializeField] private Vector3 _btnSizeChange = new Vector3(1.4f, 1.4f, 1.4f);
    [SerializeField] private AudioClip _music = null;
    [SerializeField] private AudioClip _sfxEnter = null;
    [SerializeField] private AudioClip _sfxBack = null;    
    [SerializeField] private float _sfxVolume = 1f;
    [SerializeField] private float _delayTime = .2f;    
    private AudioSource _currentAudioSource = null;
    private Coroutine _coroutine;
    private bool _isQuit = false;

    private void Start()
    {
        if( _music != null)
        {            
            _currentAudioSource = AudioHelper.PlayClip2D(_music, _sfxVolume);
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

        //_coroutine = StartCoroutine(DelayLoadScene(_sfxEnter, _optionsScene, _delayTime));      
        _coroutine = StartCoroutine(DelayLoadMenu(_optionsMenuCvs, _mainMenuCvs,
            _optionsBtn, _sfxEnter, _sfxVolume, _delayTime));
    }

    public void OptionsBackToMain()
    {    
        AnimateButton(_backBtn);
        //Add Delay;
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _delayTime = _sfxBack.length;
                      
        _coroutine = StartCoroutine(DelayLoadMenu(_mainMenuCvs, _optionsMenuCvs,
            _backBtn, _sfxBack, _sfxVolume, _delayTime));
    }

    public void QuitBackToMain()
    {
        AnimateButton(_backToMainBtn);
        //Add Delay;
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _delayTime = _sfxBack.length;

        _coroutine = StartCoroutine(DelayLoadMenu(_mainMenuCvs, _quitMenuCvs,
            _backToMainBtn, _sfxBack, _sfxVolume, _delayTime));
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
        // _coroutine = StartCoroutine(DelayLoadScene(_sfxEnter, _quitScene, _delayTime));
        _coroutine = StartCoroutine(DelayLoadMenu(_quitMenuCvs, _mainMenuCvs,
             _quitBtn, _sfxEnter, _sfxVolume, _delayTime));
    }

    public void TurnOnSound()
    {
        ResetAnimateOffBtn();
        AnimateOnBtn();
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _delayTime = _sfxEnter.length;
        _coroutine = StartCoroutine(DelayTurnOnSound(_music, _sfxEnter, _sfxVolume, _delayTime));
    }

    public void TurnOffSound()
    {
        ResetAnimateOnBtn();
        AnimateOffBtn();
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _delayTime = _sfxBack.length;
        _coroutine = StartCoroutine(DelayTurnOffSound(_sfxBack, _sfxVolume, _delayTime));
    }

    public void QuitGame()
    {
        if(_isQuit == false)
        {
            AnimateQuitBtn();
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            _delayTime = _sfxBack.length;

            _coroutine = StartCoroutine(DelayQuitGame(_sfxBack, _delayTime));
            _isQuit = true;
        }
        
    }

    private IEnumerator DelayLoadMenu(GameObject menuToOn, GameObject menuToOff,
        Button button, AudioClip sfx, float volume, float delayTime)
    {
        AudioHelper.PlayClip2D(sfx, volume);

        yield return new WaitForSeconds(delayTime);

        menuToOn.SetActive(true);
        menuToOff.SetActive(false);
        ResetAnimationButton(button);

    }

    private IEnumerator DelayTurnOnSound(AudioClip music, AudioClip sfx, float volume, float delayTime)
    {
        if (_currentAudioSource == null)
        {
            AudioHelper.PlayClip2D(sfx, volume);

            yield return new WaitForSeconds(delayTime);

            _currentAudioSource = AudioHelper.PlayClip2D(music, volume);

            _offBtn.gameObject.SetActive(true);
            _onBtn.gameObject.SetActive(false);
        }
    }

    private IEnumerator DelayTurnOffSound(AudioClip sfx, float volume, float delayTime)
    {
        if (_currentAudioSource != null)
        {
            AudioHelper.PlayClip2D(sfx, volume);

            yield return new WaitForSeconds(delayTime);

            Destroy(_currentAudioSource);

            _onBtn.gameObject.SetActive(true);
            _offBtn.gameObject.SetActive(false);

        }
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

    private IEnumerator DelayQuitGame(AudioClip clip, float delayTime)
    {
       AudioHelper.PlayClip2D(clip, _sfxVolume);

        yield return new WaitForSeconds(delayTime);

        Application.Quit();
        
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

    private void AnimateOnBtn()
    {
        _onBtn.GetComponent<RectTransform>().localScale = _btnSizeChange;

    }

    private void ResetAnimateOnBtn()
    {
        _onBtn.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

    private void AnimateOffBtn()
    {
        _offBtn.GetComponent<RectTransform>().localScale = _btnSizeChange;
    }

    private void ResetAnimateOffBtn()
    {
        _offBtn.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }    

    private void AnimateButton (Button button)
    {
        button.GetComponent<RectTransform>().localScale = _btnSizeChange;
    }

    private void ResetAnimationButton(Button button)
    {
        button.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }
}
