using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Oculus.VoiceSDK.UX;

public class RadioVariables : Variables
{
    public NetworkVariable<float> amplitude = new NetworkVariable<float>(0.05f);
    public NetworkVariable<float> frequency = new NetworkVariable<float>(10f);

    public NetworkVariable<float> targetAmp = new NetworkVariable<float>(0.05f);
    public NetworkVariable<float> targetFreq = new NetworkVariable<float>(10f);

    //tracking the lever positions to display as ghosts for other player
    public NetworkVariable<Quaternion> pastRot = new NetworkVariable<Quaternion>();
    public NetworkVariable<Quaternion> futureRot = new NetworkVariable<Quaternion>();

    public NetworkVariable<bool> futureLeverGrabbed = new NetworkVariable<bool>(false);
    public NetworkVariable<bool> pastLeverGrabbed = new NetworkVariable<bool>(false);

    public List<float> stationAmps = new List<float>() { 0.04f, 0.01f, 0.08f };
    public List<float> stationFreqs = new List<float>() { 10, 2, 21 };
    
    private int stationIndex = 0;
    public NetworkVariable<bool> codeEntered = new NetworkVariable<bool>(false);
    public NetworkVariable<ScreenState> screenState = new(ScreenState.EnterStation);

    
    public enum ScreenState
    {
        Off,
        SingleGrab,
        EnterStation,
        InvalidStation,
        MatchWaves,
    }

    [Rpc(SendTo.Server)]
    public void UpdateFrequencyServerRpc(float freq, Quaternion rot)
    {
        futureRot.Value = rot;
        frequency.Value = freq;
    }

    public void UpdateAmplitude(float amp, Quaternion rot)
    {
        pastRot.Value = rot;
        amplitude.Value = amp;
    }

    [Rpc(SendTo.Server)]
    public void OnFutureGrabServerRpc(bool isGrabbed)
    {
        futureLeverGrabbed.Value = isGrabbed;
        ChangeStationOnGrab();
    }

    public void OnPastGrab(bool isGrabbed)
    {
        pastLeverGrabbed.Value = isGrabbed;
        ChangeStationOnGrab();
    }

    public void ChangeScreen(RadioVariables.ScreenState state)
    {
        screenState.Value = state;
    }

    private void ChangeStationOnGrab()
    {
        if (pastLeverGrabbed.Value ^ futureLeverGrabbed.Value)
        {
            //screenState.Value = ScreenState.SingleGrab;
        }
        if (pastLeverGrabbed.Value || futureLeverGrabbed.Value)
        {
            if (!codeEntered.Value) screenState.Value = ScreenState.EnterStation;
            else screenState.Value = ScreenState.MatchWaves;
        } 
        else 
        {
            //screenState.Value = ScreenState.Off;
        }
    }

    private bool CheckTargetWave(float freq, float amp)
    {
        if (amplitude.Value == targetAmp.Value && frequency.Value == targetFreq.Value) { return true; }
        else return false;
    }

    private void AdvanceTargetWave(int index)
    {
        targetAmp.Value = stationAmps[index];
        targetFreq.Value = stationFreqs[index];
    }

    public void StartMatchingWaves()
    {
        codeEntered.Value = true;
        screenState.Value = ScreenState.MatchWaves;
        stationIndex = 0;
        StartCoroutine(NewMatchingLevel(stationIndex));
    }

    public IEnumerator NewMatchingLevel(int index)
    {
        Debug.Log("mathcing");
        codeEntered.Value = true;
        screenState.Value = ScreenState.MatchWaves;
        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);
        if (CheckTargetWave(frequency.Value, amplitude.Value))
        {
            stationIndex++;
            AdvanceTargetWave(stationIndex);
        } else
        {
            stationIndex = 0;
        }
    }




}
