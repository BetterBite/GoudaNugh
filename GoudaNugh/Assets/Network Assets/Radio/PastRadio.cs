using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastRadio : PastObject
{

    private RadioVariables vars;
    public Sinewave wave;
    public GameObject leverRot;
    public GameObject ghostRot;
    public override void Setup()
    {
        ghostRot.SetActive(false);
        vars = networkObject.GetComponent<RadioVariables>();
        vars.frequency.OnValueChanged += ReceiveUpdatedFrequency;
        vars.futureLeverGrabbed.OnValueChanged += ReceiveLeverGrab;
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
        vars.OnFutureGrabServerRpc(isGrabbed);
    }


}
