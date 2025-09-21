using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinVolume : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    [SerializeField] private float _delayTime = 0.2f;
    private IEnumerator _coroutine;

    private void OnCollisionEnter(Collision other)
    {
        _coroutine = StartDelay(_delayTime);

        if (other.gameObject.CompareTag("Player"))
        {
            //Add Delay before turn on Win Scene
            StartCoroutine(_coroutine);

            _gameManager.OpenWin();
        }
    }

    private IEnumerator StartDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
    }
}
