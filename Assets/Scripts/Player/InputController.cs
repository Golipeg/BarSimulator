using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _joystick;
    private Transform _camera;


    private readonly string HORIZONTAL = "Horizontal";
    private readonly string VERTICAL = "Vertical";
    private const float FULL_CIRCLE = 360f;
    private const float INVERSION_AXIS = -1f;

    private float _horizontalAxis;
    private float _verticalAxis;

    private Vector3 _direction;
    private Vector3 _offsetDirection;

    private float _cameraOffset;
    public Vector3 GetDirection => _offsetDirection;

    private void Start()
    {
        _camera = Camera.main.transform;
        _cameraOffset = FULL_CIRCLE - _camera.rotation.eulerAngles.y;
    }

    private void Update()
    {
        if (isJoystick())
        {
            _horizontalAxis = _joystick.Horizontal;
            _verticalAxis = _joystick.Vertical;
        }
        else
        {
            _horizontalAxis = Input.GetAxis(HORIZONTAL);
            _verticalAxis = Input.GetAxis(VERTICAL);
        }


        _direction = new Vector3(_horizontalAxis, 0f, _verticalAxis);
        Quaternion rotationQuaternion = Quaternion.AngleAxis(_cameraOffset * INVERSION_AXIS, Vector3.up);
        _offsetDirection = rotationQuaternion * _direction;
    }

    private bool isJoystick() => new Vector2(_joystick.Horizontal, _joystick.Vertical) != Vector2.zero;
}