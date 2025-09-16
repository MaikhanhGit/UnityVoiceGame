using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;
    [SerializeField] private float _movingSpeed = 2f;

    private Vector3 _nextPos;

    private void Start()
    {
        _nextPos = _pointB.position;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, 
            _nextPos, _movingSpeed * Time.deltaTime);

        if (transform.position == _nextPos)
        {
            _nextPos = (_nextPos == _pointA.position) ? _pointB.position : _pointA.position;
        }
    }
}
