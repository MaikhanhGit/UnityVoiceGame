using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class QuitMenu : MonoBehaviour
{
    public string _mainMenuScene = "MainMenu";
    [SerializeField] private Button _backBtn = null;
    [SerializeField] private Button _quitBtn = null;
    [SerializeField] private Vector3 _btnSizeChange = new Vector3(1.4f, 1.4f, 1.4f);
    [SerializeField] AudioClip _sfxEnter = null;
    [SerializeField] AudioClip _sfxBack = null;
    [SerializeField] float _sfxVolume = 1f;
    [SerializeField] private float _delayTime = 1.0f;
    private Coroutine _coroutine;
    private bool _isQuit = false;

    public void BackToMainMenu()
    {
        AnimateBackBtn();
        //Add Delay SFX
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _delayTime = _sfxEnter.length;
        _coroutine = StartCoroutine(DelayLoadScene(_sfxBack, _mainMenuScene, _delayTime));
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
            _isQuit = true;
            _delayTime = _sfxBack.length;
            _coroutine = StartCoroutine(DelayQuit(_sfxBack, _delayTime));
            
        }
       
    }

    private IEnumerator DelayQuit(AudioClip clip, float delayTime)
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

    private void AnimateBackBtn()
    {
        _backBtn.GetComponent<RectTransform>().localScale = _btnSizeChange;
    }

    private void AnimateQuitBtn()
    {
        _quitBtn.GetComponent<RectTransform>().localScale = _btnSizeChange;
    }

}
