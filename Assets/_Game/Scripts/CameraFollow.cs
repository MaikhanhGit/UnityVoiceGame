using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float _followSpeed = 2f;
    [SerializeField] private Transform _target = null;
    [SerializeField] private float _yOffset = 1f;

    private void Update()
    {
        Vector3 newPos = new Vector3(_target.position.x, 
            _target.position.y + _yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, _followSpeed*Time.deltaTime);
    }
}
