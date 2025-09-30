using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class ScalingGeo : MonoBehaviour
{
    [SerializeField] Vector3 _minScale;
    [SerializeField] Vector3 _maxScale;
    [SerializeField] float _loudnessSens = 10;
    [SerializeField] float _threshold = 0.1f;
    //[SerializeField] float _yRotationRate = 3f;
    [SerializeField] float _minRandom = -0.5f;
    [SerializeField] float _maxRandom = 0.5f;
    [SerializeField] GameObject[] _objectToScale = null;
    [SerializeField] AudioLoudnessDetector _detector = null;
    private int _numObjects = 0;

    private void Start()
    {
        _numObjects = _objectToScale.Length;
    }

    private void FixedUpdate()
    {
        float loudness = _detector.GetLoudnessFromMic() * _loudnessSens;

        if (loudness < _threshold)
        {
            loudness = 0;
        }
        
        ScaleObject(loudness);
        //RotateObject(loudness);
    }

    private void ScaleObject(float loudness)
    {
        if (_numObjects > 0)
        {
            for (int i = 0; i < _numObjects; i++)
            {
                float currentLoudness = loudness;
                float newLoudness = currentLoudness + Random.Range(_minRandom, _maxRandom);

               _objectToScale[i].transform.localScale = Vector3.Lerp(_minScale, _maxScale, newLoudness);

                loudness = currentLoudness;

            }
        }
        //_objectToScale.transform.localScale = Vector3.Lerp(_minScale, _maxScale, loudness);
    }

    private void RotateObject(float loudness)
    {
        //_objectToScale.transform.Rotate(0.0f, _yRotationRate * loudness, 0.0f, Space.Self);
    }




}
