using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FutureRadio : FutureObject
{
    private RadioVariables vars;

    public GameObject waveObject;
    public Sinewave wave;

    public Sinewave targetWave;
    public GameObject leverRot;
    public GameObject ghostRot;

    public GameObject screenObject;
    private RadioScreen screen;

    public LockableObject door;
    public UnityEvent solvedRadio;


    public override void Setup()
    {
        screen = screenObject.GetComponent<RadioScreen>();
        ghostRot.SetActive(false);
        vars = networkObject.GetComponent<RadioVariables>();
        vars.amplitude.OnValueChanged += ReceiveUpdatedAmplitude;
        vars.frequency.OnValueChanged += ReceiveUpdatedFrequency;


        vars.pastLeverGrabbed.OnValueChanged += ReceiveLeverGrab;
        vars.screenState.OnValueChanged += ChangeScreen;

        vars.targetAmp.OnValueChanged += ReceiveNewTargetAmp;
        vars.targetFreq.OnValueChanged += ReceiveNewTargetFreq;
        vars.matchingLevel.OnValueChanged += UpdateMatchingLevel;

        vars.waveIsValid.OnValueChanged += ReceiveWaveValidate;

        vars.radioSolved.OnValueChanged += SolveRadio;

        wave.lr.startColor = Color.red;
        wave.lr.endColor = Color.red;

    }


    public void UpdateFrequency(float freq)
    {
        vars.UpdateFrequencyServerRpc(freq, leverRot.transform.rotation);
        
    }

    private void ReceiveUpdatedFrequency(float prevFreq, float freq)
    {
        wave.frequency = freq;
        
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

    private void UpdateMatchingLevel(int prevLevel, int level)
    {
        targetWave.amplitude = vars.stationAmps[level];
        targetWave.frequency = vars.stationFreqs[level];
    }

    private void ReceiveNewTargetAmp(float prevAmp, float amp) 
    {
       // targetWave.amplitude = amp;
        
    }

    private void ReceiveNewTargetFreq(float prevFreq, float freq)
    {
        //targetWave.frequency = freq;
    }

    public void OnLeverGrab(bool isGrabbed)
    {
        vars.OnFutureGrabServerRpc(isGrabbed);
    }

    private void ChangeScreen(RadioVariables.ScreenState prevState, RadioVariables.ScreenState newState)
    {
        screen.SetScreen(newState);
    }

    private void ReceiveWaveValidate(bool wasValid, bool isValid)
    {
        if (wasValid != isValid)
        {
            if (isValid) 
            {
                wave.lr.startColor = Color.green;
                wave.lr.endColor = Color.green;
            } else
            {
                wave.lr.startColor = Color.red;
                wave.lr.endColor = Color.red;
            }
        }
    }

    private void SolveRadio(bool wasSolved, bool isSolved)
    {
        if (isSolved)
        {
            solvedRadio.Invoke();
            door.Unlock();
        }
    }


}
