using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinVolume : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    [SerializeField] private float _delayTime = 0.2f;
    [SerializeField] private ParticleSystem _winParticle = null;
    [SerializeField] private AudioClip _winSFX = null;
    [SerializeField] private float _sfxVolume = 1f;
    private Coroutine _coroutine;
    private string _winSceneName = "WinScene";

    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.CompareTag("Player"))
        {
            //Add Delay before turn on Win Scene
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _delayTime = _winSFX.length;

            _coroutine = StartCoroutine(DelayLoadScene(_winSFX, _sfxVolume, _winSceneName, _delayTime));
        }
    }

    private IEnumerator DelayLoadScene(AudioClip clip, float volume, string sceneName, float delayTime)
    {        
        AudioHelper.PlayClip2D(clip, _sfxVolume);
        _winParticle.Play();

        yield return new WaitForSeconds(delayTime);

        SceneManager.LoadScene(sceneName);
    }

    
}
