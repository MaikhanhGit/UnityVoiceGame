using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillVolume : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    [SerializeField] private float _delayTime = 0.3f;
    private IEnumerator _coroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {            
            _coroutine = StartDelay(_delayTime);

            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if ( (playerController != null))
            {
                //Add Delay before turn on Win Scene
                StartCoroutine(_coroutine);
                playerController.KillPlayer();
            }
            
        }
    }

    private IEnumerator StartDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
    }
}
