using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] private float _delayTime = 0.3f;    
    [SerializeField] AudioClip _sfxKilled = null;
    [SerializeField] float _sfxVolume = 1f;
    [SerializeField] ParticleSystem _killedParticles = null;
    [SerializeField] GameObject _playerVisualToDisable = null;
    private float _maxLoudnessAllowed;
    private Rigidbody _rigid;
    private Transform _iniTransform;
    private bool _isKilled = false;
 
    private Coroutine _coroutine;


    private void Start()
    {
        _isKilled = false;
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

 

    public void KillPlayer()
    {
        if(_isKilled == false)
        {
            //Add Delay SFX
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            _delayTime = _sfxKilled.length;
            _coroutine = StartCoroutine(DelayLoadScene(_sfxKilled, "Sandbox2", _delayTime));
            _isKilled = true;
            _killedParticles.Play();
            _playerVisualToDisable.SetActive(false);
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
        SceneManager.LoadScene(sceneName);
    }
}
