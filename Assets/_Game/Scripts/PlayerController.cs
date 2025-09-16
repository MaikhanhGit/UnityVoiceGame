using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 0.1f;
    [SerializeField] private float _rollSpeedMultiplier = 5f;
    [SerializeField] AudioLoudnessDetector _loudnessDetector = null;    
    [SerializeField] private float _loudnessSens = 40;
    [SerializeField] private float _rollThreshold = 0.1f;
    [SerializeField] private float _jumpThreshold = 0.5f;
    [SerializeField] private float _jumpPercent = 0.1f;
    [SerializeField] private float _gravityAdd = 2f;
    [SerializeField] private GameManager _gameManager;

    private Rigidbody _rigid;
    private Transform _iniTransform;

    private void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        _iniTransform = GetComponent<Transform>();
    }


    private void Update()
    {
        float loudness = _loudnessDetector.GetLoudnessFromMic() * _loudnessSens;

        if (loudness < _rollThreshold)
        {
            loudness = 0;
        }
        else if (loudness < _jumpThreshold)
        {
            RollBall(loudness);
        }
        else if (loudness >= _jumpThreshold)
        {
            ThrustBall(loudness);
        }
        
    }   

   
    private void FixedUpdate()
    {
        // make ball heavier by adding downward force
        _rigid.AddForce(Physics.gravity * _gravityAdd, ForceMode.Acceleration);


    }

    private void RollBall(float loudness)
    {
        _rigid.AddForce(Vector3.right * (_speed * _rollSpeedMultiplier + loudness));
    }

    private void ThrustBall(float loudness)
    {
        _rigid.AddForce(Vector3.right * (_speed + loudness) * _jumpPercent/2);
        _rigid.AddForce(Vector3.up * (_speed + loudness * _jumpPercent));
    }

    public void KillPlayer()
    {
        Debug.Log("Kill Player");
        RestartGame();
        Transform currentTrans = gameObject.GetComponent<Transform>();
        gameObject.GetComponent<Transform>().SetPositionAndRotation(_iniTransform.position, _iniTransform.rotation);
        _rigid.velocity = new Vector3(0, 0, 0);
        
    }

    private void RestartGame()
    {
        _gameManager.StartGame();
    }

}
