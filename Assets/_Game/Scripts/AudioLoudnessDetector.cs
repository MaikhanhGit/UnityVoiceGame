using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Android;

public class AudioLoudnessDetector : MonoBehaviour
{        
    private int _sampleWindow = 64;
    private AudioClip _micAudio = null;
    private string _micName = null;
    public float _audLoudness = 0f;
       
    [SerializeField] private float _loudnessSens = 40;
    [SerializeField] public float _maxLoudnessAllowed = 5f;
    [SerializeField] private float _rollThreshold = 0.1f;      
    [SerializeField] private float _jumpThreshold = 3f;
    [SerializeField] private float _mouthScaleSens = 1f;
    [SerializeField] private GameObject _player = null;
    
    private PlayerController _playerController = null;

    private void Start()
    {
        _playerController = _player.GetComponent<PlayerController>();

    }

    private void FixedUpdate()
    {
        float loudness = this.GetLoudnessFromMic() * _loudnessSens;

        if (loudness < _rollThreshold)
        {
            loudness = 0;
        }
        else if (loudness < _jumpThreshold && loudness >= _rollThreshold)
        {
            _playerController.RollBall(loudness);
            _playerController.ScaleRollMouth(loudness);
        }

        else if (loudness >= _jumpThreshold)
        {
            _playerController.ThrustBall(loudness);
            _playerController.ScaleJumpMouth(loudness);
        }
        
    }

    public bool GetMicInput()
    {
        if (Microphone.devices.Length > 0)
        {
            _micName = Microphone.devices[0];
            
            _micAudio = Microphone.Start(_micName, true, 10, AudioSettings.outputSampleRate);

            return true;
            //GetLoudnessFromMic();
        }
        else
        {
            Debug.Log("No Microphone Detected");
            return false;
        }       
    }

    public void GetMicPermission()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
        else
        {
            return;
        }
    }    

    public float GetLoudnessFromMic()
    {
        if (Microphone.devices[0] != null)
        {
            return GetAudioLoudness(Microphone.GetPosition(Microphone.devices[0]), _micAudio);            
        }
        else
        {
            Debug.Log("No Mic Detected");

            return 0f;
        }
       
    }

    public float GetAudioLoudness(int clipPosition, AudioClip clip)
    {        
        int startPosition = clipPosition - _sampleWindow;        

        if (startPosition < 0)
        {
            _audLoudness = 0f;
            return _audLoudness;
        }
        else
        {
            float[] waveData = new float[_sampleWindow];

            clip.GetData(waveData, startPosition);

            //compute loudness
            float totalLoudness = 0;

            for (int i = 0; i < _sampleWindow; i++)
            {
                totalLoudness += Mathf.Abs(waveData[i]);
            }

            _audLoudness = totalLoudness / _sampleWindow;
            return _audLoudness;
        }
            
    }

}
