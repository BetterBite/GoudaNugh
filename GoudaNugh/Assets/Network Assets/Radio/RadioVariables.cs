using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Oculus.VoiceSDK.UX;

public class RadioVariables : Variables
{
    public NetworkVariable<float> amplitude = new NetworkVariable<float>(0.05f);
    public NetworkVariable<float> frequency = new NetworkVariable<float>(10f);

    //tracking the lever positions to display as ghosts for other player
    public NetworkVariable<Quaternion> pastRot = new NetworkVariable<Quaternion>();
    public NetworkVariable<Quaternion> futureRot = new NetworkVariable<Quaternion>();

    public NetworkVariable<bool> futureLeverGrabbed = new NetworkVariable<bool>(false);
    public NetworkVariable<bool> pastLeverGrabbed = new NetworkVariable<bool>(false);

    public NetworkVariable<bool> radioOn = new(false);

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
        toggleRadio();
    }

    public void OnPastGrab(bool isGrabbed)
    {
        pastLeverGrabbed.Value = isGrabbed;
        toggleRadio();
    }

    private void toggleRadio()
    {
        if (pastLeverGrabbed.Value && futureLeverGrabbed.Value)
        {
            radioOn.Value = true;
        } else
        {
            radioOn.Value = false;
        }
    }


}
