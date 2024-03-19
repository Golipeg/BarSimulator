using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraSmoothFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothSpeed = 0.1f;
    private Vector3 _distance;

    private void Start()
    {
        _distance = transform.position - _target.position;
    }

    void LateUpdate()
    {
        if (_target != null)
        {
            Vector3 desiredPosition = _target.position + _distance;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);

            transform.position = smoothedPosition;

            transform.LookAt(_target);
        }
    }
}