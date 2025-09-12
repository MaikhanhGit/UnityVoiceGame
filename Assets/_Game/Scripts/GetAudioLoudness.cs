using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class GetAudioLoudness : MonoBehaviour
{
    [SerializeField] GetAudioSignal _getAudioSignal = null;
    [SerializeField] int _sampleWindow = 64;

    public float GetLoudness()
    {
        float totalLoudness = 0;

        //check if there's a mic input
        string micName = _getAudioSignal.GetMicInput();      

        if (micName != null)
        {
            AudioClip audioSignal = _getAudioSignal.GetMicAudio(micName);

            int micPos = Microphone.GetPosition(Microphone.devices[0]);           

            int startPosition = micPos - _sampleWindow;            

            if (startPosition < 0)
            {            
                return 0f;
            }
            float[] waveData = new float[_sampleWindow];

            audioSignal.GetData(waveData, startPosition);

            //compute loudness  
            for (int i = 0; i < _sampleWindow; i++)
            {
                totalLoudness += Mathf.Abs(waveData[i]);
            }

            return totalLoudness / _sampleWindow;
        }
        else
        {
            Debug.Log("No microphone detected");
            return 0;
        }

    }           
    
}
