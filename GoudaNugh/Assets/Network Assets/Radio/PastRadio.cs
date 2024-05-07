using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PastRadio : PastObject
{

    private RadioVariables vars;
    public Sinewave wave;
    public Sinewave targetWave;
    public GameObject leverRot;
    public GameObject ghostRot;

    public GameObject screenObject;
    private RadioScreen screen;

    public RadioTimer timer;
    public LockableObject door;

    public UnityEvent solvedRadio;

    private List<int> Code = new List<int>();

    public int stationIndex = 0;
    public override void Setup()
    {
        screen = screenObject.GetComponent<RadioScreen>();
        ghostRot.SetActive(false);
        //wave.GetComponent<GameObject>().SetActive(false);
        
        vars = networkObject.GetComponent<RadioVariables>();
        vars.frequency.OnValueChanged += ReceiveUpdatedFrequency;
        vars.amplitude.OnValueChanged += ReceiveUpdatedAmplitude;

        vars.futureLeverGrabbed.OnValueChanged += ReceiveLeverGrab;
        vars.screenState.OnValueChanged += ChangeScreen;

        vars.targetAmp.OnValueChanged += ReceiveNewTargetAmp;
        vars.targetFreq.OnValueChanged += ReceiveNewTargetFreq;
        vars.matchingLevel.OnValueChanged += UpdateMatchingLevel;
        vars.waveIsValid.OnValueChanged += ReceiveWaveValidate;

        vars.radioSolved.OnValueChanged += SolveRadio;

        wave.lr.startColor = Color.red;
        wave.lr.endColor = Color.red;

        vars.matchingLevel.Value = -1;
        timer.ResetTimers();

    }

    private void ReceiveUpdatedFrequency(float prevFreq, float currFreq) 
    { 
        wave.frequency = currFreq;
        ghostRot.transform.rotation = vars.futureRot.Value;
    }

    private void ReceiveUpdatedAmplitude(float prevAmp, float amp)
    {
        wave.amplitude =  amp;
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
        screen.SetScreen(newState);
    }

    private void UpdateMatchingLevel(int prevLevel, int level)
    {
        if (level == -1)
        {
            timer.ResetTimers();
        }
        if (level < 0 || level > 2) return;

        //targetWave.amplitude = vars.stationAmps[level];
        //targetWave.frequency = vars.stationFreqs[level];
        Debug.Log("filling:" + level);
        timer.NextNumber(level);
    }

    private void ReceiveNewTargetAmp(float prevAmp, float amp)
    {
        targetWave.amplitude = amp;

    }

    private void ReceiveNewTargetFreq(float prevFreq, float freq)
    {
       targetWave.frequency = freq;
    }

    public void EnterRadioNumber(int n)
    {
        if (screen.activeScreen != RadioVariables.ScreenState.EnterStation) return;
        screen.DisplayEnteredNumber(n);
        Code.Add(n);
        if (Code.Count > 2)
        {
            if (CheckStationCode())
            {
                vars.StartMatchingWaves();
            }
            else screen.ResetScreenOnWrongCode();
        }
    }

    private bool CheckStationCode()
    {
        int[] answer = new int[] { 3, 1, 4 };
        for (int i = 0; i < Code.Count; i++)
        {
            if (Code[i] != answer[i])
            {
                Debug.Log("code is wrong");
                Code.Clear();
                StartCoroutine(screen.InvalidateStation());
                return false;
            }
        }
        return true;
    }

    private void ReceiveWaveValidate(bool wasValid, bool isValid)
    {
        if (wasValid != isValid)
        {
            if (isValid)
            {
                wave.lr.startColor = Color.green;
                wave.lr.endColor = Color.green;
            }
            else
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
