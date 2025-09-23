using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    [SerializeField] GameObject _objectToActivate;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(_objectToActivate.activeSelf == false)
            {
                _objectToActivate.SetActive(true);
            }
            
        }
    }
}
