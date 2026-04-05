using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float rotateSpeed = 20.0f, speed = 10.0f, zoomspeed = 10.0f;

    private float _mult = 1f;

    private void Update(){

        float hor = 0f, ver = 0f;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) hor = -1;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) hor = 1;
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) ver = 1;
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) ver = -1;

        float rotate = 0f;

        if (Keyboard.current.qKey.isPressed)
            rotate = -1f;
        else if (Keyboard.current.eKey.isPressed)
            rotate = 1f;

        _mult = Keyboard.current.leftShiftKey.isPressed ? 2 : 1;

        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime * rotate * _mult, Space.World);
        transform.Translate(new Vector3(hor,0,ver) * Time.deltaTime * _mult * speed, Space.Self);

        transform.position += transform.up * zoomspeed * Time.deltaTime * Mouse.current.scroll.ReadValue().y;

        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(transform.position.y,-2f,30f),
            transform.position.z);
        
    }
}
