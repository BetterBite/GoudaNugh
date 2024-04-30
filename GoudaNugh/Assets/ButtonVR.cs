using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


public class ButtonVR : MonoBehaviour
{
    public UnityEvent onPress;
    public UnityEvent onRelease;
    [SerializeField] private float threshold = .1f;
    [SerializeField] private float deadzone = .025f;
    private bool isPressed;
    private Vector3 startPos;
    private Vector3 pressedPos;
    private ConfigurableJoint joint;


    // Start is called before the first frame update
    void Start()
    {
        isPressed = false;
        startPos = transform.localPosition;
        pressedPos = transform.localPosition - new Vector3(0, .03f, 0);
        joint = GetComponent<ConfigurableJoint>();
    }

    void Update () 
    { 
        if (!isPressed && GetValue() + threshold >= 1)
        {
            Pressed();
        }
        if (isPressed && GetValue() - threshold <= 0)
        {
            Released();
        }
    }

    private float GetValue()
    {
        var value = Vector3.Distance(startPos, transform.localPosition) / joint.linearLimit.limit;
        if (Math.Abs(value) < deadzone)
        {
            value = 0;
        }

        return Mathf.Clamp(value, -1f, 1f);
    }

    public void Grabbed()
    {
        transform.localPosition = pressedPos;
        //isPressed = true;
        //onPress.Invoke();
        //Debug.Log("Button Pressed");
        //transform.localPosition = startPos;

    }



    public void Pressed()
    {
        isPressed = true;
        onPress.Invoke();
    }

    public void Released()
    {
        isPressed = false;
        onRelease.Invoke();
    }



}
