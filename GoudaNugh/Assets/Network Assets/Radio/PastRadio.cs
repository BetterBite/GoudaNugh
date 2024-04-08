using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastRadio : PastObject
{

    private RadioVariables vars;
    public Sinewave wave;

    public override void Setup()
    {
        vars = networkObject.GetComponent<RadioVariables>();
        vars.frequency.OnValueChanged += ReceiveUpdatedFrequency;
    }

    private void ReceiveUpdatedFrequency(float prevFreq, float currFreq) 
    { 
        wave.frequency = currFreq;
    }

    public void UpdateAmplitude(float amp)
    {
        vars.UpdateAmplitude(amp);
    }



   
}
