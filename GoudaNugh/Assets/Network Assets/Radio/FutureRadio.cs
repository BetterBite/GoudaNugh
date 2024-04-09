using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutureRadio : FutureObject
{
    private RadioVariables vars;
    public Sinewave wave;
    public override void Setup()
    {
        vars = networkObject.GetComponent<RadioVariables>();
        vars.amplitude.OnValueChanged += ReceiveUpdatedAmplitude;
    }

    public void UpdateFrequency(float freq)
    {
        vars.UpdateFrequencyServerRpc(freq);
    }

    private void ReceiveUpdatedAmplitude(float prevAmp, float currAmp)
    {
        wave.amplitude = currAmp;
    }
    
}
