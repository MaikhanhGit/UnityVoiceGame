using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossActivator : MonoBehaviour
{
    [SerializeField] GameObject _objectToActivate;
    [SerializeField] private GameObject _flashImage = null;
    [SerializeField] private float _delayTime = 0.3f;
    [SerializeField] private AudioClip _activateSFX = null;
    private Coroutine _coroutine;
    private bool _isBossActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        AudioHelper.PlayClip2D(_activateSFX, 1f);

        if(other.tag == "Player")
        {
            if(_isBossActivated == false)
            {
                _isBossActivated = true;
                if (_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                }

                _coroutine = StartCoroutine(DelayFlashScreen());
            }
                                       
        }
    }

    private IEnumerator DelayFlashScreen()
    {  
        _flashImage.SetActive(true);
       
        yield return new WaitForSeconds(_delayTime);

        _flashImage.SetActive(false);

        if (_objectToActivate.activeSelf == false)
        {
            _objectToActivate.SetActive(true);
        }

        gameObject.SetActive(false);
    }
}
