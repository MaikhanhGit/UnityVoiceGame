using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEffect : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _height = 0.2f;
    private Vector3 _startPos;

    private void Start()
    {
        _startPos = transform.position;
    }

    private void FixedUpdate()
    {
        float newY = _startPos.y + Mathf.Sin(Time.time * _speed) + _height;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
