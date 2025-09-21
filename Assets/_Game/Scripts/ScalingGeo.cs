using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class ScalingGeo : MonoBehaviour
{
    [SerializeField] Vector3 _minScale;
    [SerializeField] Vector3 _maxScale;
    [SerializeField] float _loudnessSens = 250;
    [SerializeField] float _threshold = 0.1f;
    [SerializeField] float _yRotationRate = 15f;
    [SerializeField] GameObject _objectToScale = null;
    [SerializeField] AudioLoudnessDetector _detector = null;

    private void Update()
    {
        float loudness = _detector.GetLoudnessFromMic() * _loudnessSens;

        if (loudness < _threshold)
        {
            loudness = 0;
        }

        ScaleObject(loudness);
        RotateObject(loudness);
    }

    private void ScaleObject(float loudness)
    {
        _objectToScale.transform.localScale = Vector3.Lerp(_minScale, _maxScale, loudness);
    }

    private void RotateObject(float loudness)
    {
        _objectToScale.transform.Rotate(0.0f, _yRotationRate * loudness, 0.0f, Space.Self);
    }




}
