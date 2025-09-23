using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _rollSpeedMultiplier = 1.75f;       
    [SerializeField] private float _jumpPercent = 4.5f;    
    [SerializeField] private float _gravityAdd = 1.5f;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] AudioLoudnessDetector _loudnessDetector = null;
    [SerializeField] private GameObject _mouth;
    [SerializeField] Vector3 _rollMinScale;
    [SerializeField] Vector3 _rollMaxScale;
    [SerializeField] Vector3 _jumpMinScale;
    [SerializeField] Vector3 _jumpMaxScale;
    [SerializeField] private float _jumpMouSclSens = 0.05f;
    [SerializeField] private float _rollMouSclSens = 0.1f;
    private float _maxLoudnessAllowed;
    private Rigidbody _rigid;
    private Transform _iniTransform;
    private float _currentSpeed = 1f;
  

    private void Start()
    {
        _rigid = gameObject.GetComponentInChildren<Rigidbody>();
        _iniTransform = gameObject.GetComponentInChildren<Transform>();
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

        ScaleRollMouth(loudness);
       
    }

    public void ThrustBall(float loudness)
    {       
        if (loudness > _maxLoudnessAllowed)
        {            
            loudness = _maxLoudnessAllowed;                                      
        }       
        
        ScaleJumpMouth(loudness);

        _rigid.AddForce(Vector3.right * (_speed + loudness) * _jumpPercent/3);
        _rigid.AddForce(Vector3.up * (_speed + loudness * _jumpPercent));       
        
    }

    public void ScaleRollMouth(float loudness)
    {       

        _mouth.transform.localScale = 
          Vector3.Lerp(_rollMinScale, _rollMaxScale, loudness * _rollMouSclSens);
    }

    public void ScaleJumpMouth(float loudness)
    {       
        _mouth.transform.localScale = 
            Vector3.Lerp(_jumpMinScale, _jumpMaxScale, loudness * _jumpMouSclSens);        
    }

    public void PausePlayer()
    {
        _currentSpeed = _speed;
        _speed = 0;
        _rigid.velocity = new Vector3(0, 0, 0);
        
    }

    public void UnPausePlayer()
    {
        _speed = _currentSpeed;
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
