using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 0.1f;
    [SerializeField] private float _rollSpeedMultiplier = 1.5f;       
    [SerializeField] private float _jumpPercent = 3f;    
    [SerializeField] private float _gravityAdd = 2f;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] AudioLoudnessDetector _loudnessDetector = null;
    private float _maxLoudnessAllowed;
    private Rigidbody _rigid;
    private Transform _iniTransform;

    private void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        _iniTransform = GetComponent<Transform>();
        _maxLoudnessAllowed = _loudnessDetector._maxLoudnessAllowed;
    }


    private void FixedUpdate()
    {
        // make ball heavier by adding downward force
        _rigid.AddForce(Physics.gravity * _gravityAdd, ForceMode.Acceleration);
                        
    }      
   

    public void RollBall(float loudness)
    {
        _rigid.AddForce(Vector3.right * (_speed * _rollSpeedMultiplier + loudness));        
       
    }

    public void ThrustBall(float loudness)
    {       
        if (loudness > _maxLoudnessAllowed)
        {
            Debug.Log("Loudness: " + loudness);
            loudness = _maxLoudnessAllowed;                                      
        }
        Debug.Log("Loudness Used: " + loudness);
        
        _rigid.AddForce(Vector3.right * (_speed + loudness) * _jumpPercent/2);
        _rigid.AddForce(Vector3.up * (_speed + loudness * _jumpPercent));       
        
    }

    public void KillPlayer()
    {             
        Transform currentTrans = gameObject.GetComponent<Transform>();
        gameObject.GetComponent<Transform>().SetPositionAndRotation(_iniTransform.position, _iniTransform.rotation);
        _rigid.velocity = new Vector3(0, 0, 0);

        RestartGame();

    }

    private void RestartGame()
    {
        _gameManager.StartGame();
    }

}
