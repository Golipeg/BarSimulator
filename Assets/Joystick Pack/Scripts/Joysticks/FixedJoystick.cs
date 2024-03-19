using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedJoystick : Joystick
{

    private Action<FixedJoystick> _onPointerDown;
    private Action<FixedJoystick> _onPointerUp;
    public bool onPointerDown;

   
    public void InitDown(Action<FixedJoystick> onDown)
    {
        _onPointerDown = onDown;
    }
    public void InitUp(Action<FixedJoystick> onUp)
    {
        _onPointerUp = onUp;
    }

    protected override void Start()
    {
        base.Start();        
        onPointerDown = false;
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        onPointerDown = true;
        base.OnPointerDown(eventData);        
        _onPointerDown(this);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        _onPointerUp(this);
        onPointerDown = false;
    }

}