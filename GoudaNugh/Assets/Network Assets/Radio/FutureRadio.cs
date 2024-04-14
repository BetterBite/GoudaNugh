using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutureRadio : FutureObject
{
    private RadioVariables vars;
    public GameObject waveObject;
    public Sinewave wave;
    public Sinewave targetWave;
    public GameObject leverRot;
    public GameObject ghostRot;
    public GameObject Screen;


    public override void Setup()
    {

        ghostRot.SetActive(false);
        vars = networkObject.GetComponent<RadioVariables>();
        vars.amplitude.OnValueChanged += ReceiveUpdatedAmplitude;
        vars.pastLeverGrabbed.OnValueChanged += ReceiveLeverGrab;
        vars.screenState.OnValueChanged += ChangeScreen;

    }

    public void UpdateFrequency(float freq)
    {
        vars.UpdateFrequencyServerRpc(freq, leverRot.transform.rotation);
        
    }

    private void ReceiveUpdatedAmplitude(float prevAmp, float currAmp)
    {
        wave.amplitude = currAmp;
        ghostRot.transform.rotation = vars.pastRot.Value;
    }

    private void ReceiveLeverGrab(bool wasGrabbed, bool isGrabbed)
    {
        if (isGrabbed) ghostRot.SetActive(true);
        else ghostRot.SetActive(false);

    }

    public void OnLeverGrab(bool isGrabbed)
    {
        vars.OnFutureGrabServerRpc(isGrabbed);
    }

    private void ChangeScreen(RadioVariables.ScreenState prevState, RadioVariables.ScreenState newState)
    {
        if (newState == RadioVariables.ScreenState.SingleGrab)
        {

        }
        if (newState == RadioVariables.ScreenState.EnterStation)
        {
            Screen.SetActive(true);
        }
        if (newState == RadioVariables.ScreenState.Game1)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        
    }

}
