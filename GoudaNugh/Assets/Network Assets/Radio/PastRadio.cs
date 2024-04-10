using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastRadio : PastObject
{

    private RadioVariables vars;
    public Sinewave wave;
    public GameObject leverRot;
    public GameObject ghostRot;
    public GameObject Screen;
    public override void Setup()
    {
        Screen.GetComponent<RadioScreen>().activeScreen = null;
        ghostRot.SetActive(false);
        //wave.GetComponent<GameObject>().SetActive(false);
        
        vars = networkObject.GetComponent<RadioVariables>();
        vars.frequency.OnValueChanged += ReceiveUpdatedFrequency;
        vars.futureLeverGrabbed.OnValueChanged += ReceiveLeverGrab;
        vars.screenState.OnValueChanged += ChangeScreen;
        
    }

    private void ReceiveUpdatedFrequency(float prevFreq, float currFreq) 
    { 
        wave.frequency = currFreq;
        ghostRot.transform.rotation = vars.futureRot.Value;
    }

    public void UpdateAmplitude(float amp)
    {
        vars.UpdateAmplitude(amp, leverRot.transform.rotation);
    }

    private void ReceiveLeverGrab(bool wasGrabbed, bool isGrabbed)
    {
        if (isGrabbed) ghostRot.SetActive(true);
        else ghostRot.SetActive(false);
        
    }

    public void OnLeverGrab(bool isGrabbed)
    {
        vars.OnPastGrab(isGrabbed);
    }

    private void ChangeScreen(RadioVariables.ScreenState prevState, RadioVariables.ScreenState newState)
    {
        if (newState == RadioVariables.ScreenState.EnterStation)
        {
            Screen.SetActive(true);
        }
    }


}
