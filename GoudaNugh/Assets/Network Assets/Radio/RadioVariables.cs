using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class RadioVariables : Variables
{
    // Start is called before the first frame update
    public NetworkVariable<float> amplitude = new NetworkVariable<float>(0.05f);
    public NetworkVariable<float> frequency = new NetworkVariable<float>(10f);

    [Rpc(SendTo.Server)]
    public void UpdateFrequencyServerRpc(float freq)
    {
        frequency.Value = freq;
    }

    public void UpdateAmplitude(float amp)
    {
        amplitude.Value = amp;
    } 

}
