using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class AudioLoudnessDetector : MonoBehaviour
{    
    [SerializeField] bool _useMic = true;
    public int _sampleWindow = 64;
    private AudioClip _micAudio = null;
    public string _micName = null;

    private void Start()
    {
        GetMicPermission();

        GetMicInput();

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

    public void GetMicInput()
    {        
        if (_useMic)
        {
            if (Microphone.devices.Length > 0)
            {
                _micName = Microphone.devices[0];

                _micAudio = Microphone.Start(_micName, true, 10, AudioSettings.outputSampleRate);
                GetLoudnessFromMic();
            }
            else
            {
                Debug.Log("No Microphone Detected");
                return;
            }
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
            return 0;
        }

        float[] waveData = new float[_sampleWindow];

        clip.GetData(waveData, startPosition);

        //compute loudness
        float totalLoudness = 0;

        for (int i = 0; i < _sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }

        return totalLoudness / _sampleWindow;
    }

}
