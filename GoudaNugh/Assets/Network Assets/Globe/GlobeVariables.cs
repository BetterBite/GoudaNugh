using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GlobeVariables : Variables
{
    public NetworkVariable<Vector3> rotation = new NetworkVariable<Vector3>();
    //public NetworkVariable<Vector3> targetHoriz = new NetworkVariable<Vector3>();
    public NetworkVariable<Vector3> targetRot = new NetworkVariable<Vector3>();   
    public NetworkVariable<bool> globeOn = new NetworkVariable<bool>(false);

    public NetworkVariable<bool> futureLeverGrabbed = new NetworkVariable<bool>(false);
    public NetworkVariable<bool> pastLeverGrabbed = new NetworkVariable<bool>(false);

    public NetworkVariable<GlobeStates> globeState = new NetworkVariable<GlobeStates>();

    public NetworkVariable<Quaternion> pastRot = new NetworkVariable<Quaternion>();
    public NetworkVariable<Quaternion> futureRot = new NetworkVariable<Quaternion>();

    public enum GlobeStates
    {
        Unactivated,
        SingleActivated,
        Activated
    }

    public void PastMove(Vector3 vec, Quaternion rot)
    {
        pastRot.Value = rot;
        rotation.Value += vec;
    }

    [Rpc(SendTo.Server)]
    public void FutureLeverGrabbedServerRpc(bool isGrabbed)
    {
        
        futureLeverGrabbed.Value = isGrabbed;
    }

    public void PastLeverGrabbed(bool isGrabbed)
    {
        pastLeverGrabbed.Value = isGrabbed;
    }

    [Rpc(SendTo.Server)]
    public void FutureMoveServerRpc(Vector3 vec, Quaternion rot) 
    {
        futureRot.Value = rot;
        rotation.Value += vec;
    }

    public void GlobeOn()
    {
        globeOn.Value = true;
    }

    public void LeverActivated()
    {
        if (futureLeverGrabbed.Value && pastLeverGrabbed.Value)
        {
            globeOn.Value = true;
        } else
        {
            globeOn.Value= false;
        }
    }

    private void Update()
    {

        if (globeOn.Value)
        {
            //targetHoriz.Value += new Vector3(0, 0.1f, 0);
            float prog = Mathf.Sin(Time.timeSinceLevelLoad) / 2 + 0.5f;
            float vertRot = Mathf.Lerp(0, 70, prog);
            targetRot.Value = new Vector3(0, targetRot.Value.y + 0.1f, vertRot);
            //vert.Rotate(0, 0, prog);
        }      
    }


}