using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutureRadio : FutureObject
{
    private RadioVariables vars;
    public Sinewave wave;
    public GameObject leverRot;
    public GameObject ghostRot;
    public override void Setup()
    {
        ghostRot.SetActive(false);
        vars = networkObject.GetComponent<RadioVariables>();
        vars.amplitude.OnValueChanged += ReceiveUpdatedAmplitude;
        vars.pastLeverGrabbed.OnValueChanged += ReceiveLeverGrab;
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
    
}
