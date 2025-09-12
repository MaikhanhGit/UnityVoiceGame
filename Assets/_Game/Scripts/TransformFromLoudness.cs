using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformFromLoudness : MonoBehaviour
{
    [SerializeField] Vector3 _minScale;
    [SerializeField] Vector3 _maxScale;
    [SerializeField] float _loudnessSens = 250;
    [SerializeField] float _threshold = 0.1f;
    [SerializeField] GetAudioLoudness _audioLoudness = null;

    private void Update()
    {
        float loudness = _audioLoudness.GetLoudness() * _loudnessSens;

        if (loudness < _threshold)
        {
            Debug.Log("Loudness = 0!");
            loudness = 0;
        }
        else
        {
            ScaleObject(loudness);
        }
            
    }

    private void ScaleObject(float loudness)
    {
        Debug.Log("Changing Scale!");
        transform.localScale = Vector3.Lerp(_minScale, _maxScale, loudness);
    }

}
