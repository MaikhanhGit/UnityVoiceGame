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
        foreach (var micName in Microphone.devices)
        {
            Debug.Log(micName);
        }
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
            }
        }
        else
        {
            Debug.Log("Do you want to use the Phone's Microphone?");
        }


    }

    public float GetLoudnessFromMic()
    {
        return GetAudioLoudness(Microphone.GetPosition(Microphone.devices[0]), _micAudio);
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
