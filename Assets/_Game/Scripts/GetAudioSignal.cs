using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class GetAudioSignal : MonoBehaviour
{      
    private void Start()
    {
        GetMicPermission();
    }
    private void GetMicPermission()
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

    public string GetMicInput()
    {      
        if (Microphone.devices.Length > 0)
        {
            string micName = Microphone.devices[0];        
            return micName;
            
        }
        else
        {
            Debug.Log("No Microphone Detected");
            return null;
        }
        
    }
    
    public AudioClip GetMicAudio(string micName)
    {
        AudioClip _micAudio = Microphone.Start(micName, true, 10, AudioSettings.outputSampleRate);
        
        return _micAudio;        
        
    }
}
