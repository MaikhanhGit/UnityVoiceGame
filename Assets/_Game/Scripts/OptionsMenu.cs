using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;


public class OptionsMenu : MonoBehaviour
{
    public string _mainMenuScene = "MainMenu";

    [SerializeField] private Button _onBtn = null;
    [SerializeField] private Button _offBtn = null;
    [SerializeField] private Button _backBtn = null;
    [SerializeField] private AudioClip _music = null;
    [SerializeField] private AudioClip _sfxEnter = null;
    [SerializeField] private AudioClip _sfxBack = null;
    [SerializeField] private float _sfxVolume = 1f;
    [SerializeField] private float _initDelayTime = .2f;
    [SerializeField] private Vector3 _btnSizeChange = new Vector3(1.4f, 1.4f, 1.4f);
    private float _delayTime = 0f;
    private Coroutine _coroutine;
    private AudioSource _currentAudioSource = null;

    private void Start()
    {
        _currentAudioSource = AudioHelper.PlayClip2D(_music, _sfxVolume);

    }


    public void BackToMainMenu()
    {
        AnimateBackBtn();
        //Add Delay SFX
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _delayTime = _sfxBack.length;
        _coroutine = StartCoroutine(DelayLoadScene(_sfxBack, _mainMenuScene, _delayTime));
        
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
        _delayTime = _initDelayTime;
        SceneManager.LoadScene(sceneName);
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

    private void AnimateBackBtn()
    {
        _backBtn.GetComponent<RectTransform>().localScale = _btnSizeChange;
    }

   
}
