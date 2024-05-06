using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GlobeVariables : Variables
{
    public NetworkVariable<Vector3> rotation = new NetworkVariable<Vector3>();
    //public NetworkVariable<Vector3> targetHoriz = new NetworkVariable<Vector3>();
    public NetworkVariable<Vector3> targetRot = new NetworkVariable<Vector3>();  

    public NetworkVariable<bool> futureLeverGrabbed = new NetworkVariable<bool>(false);
    public NetworkVariable<bool> pastLeverGrabbed = new NetworkVariable<bool>(false);

    public NetworkVariable<GlobeStates> globeState = new NetworkVariable<GlobeStates>(GlobeStates.Unactivated);

    public NetworkVariable<Quaternion> pastRot = new NetworkVariable<Quaternion>();
    public NetworkVariable<Quaternion> futureRot = new NetworkVariable<Quaternion>();

    public enum GlobeStates
    {
        Unactivated,
        SingleActivated,
        Activated,
        Solved,
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void ChangeStateServerRpc(GlobeStates state)
    {
        globeState.Value = state;
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void AdvanceSunMoonServerRpc()
    {
        globeState.Value ++;
    }

    public void PastMove(Vector3 vec, Quaternion rot)
    {
        pastRot.Value = rot;
        rotation.Value += vec;
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void FutureLeverGrabbedServerRpc(bool isGrabbed)
    {
        
        futureLeverGrabbed.Value = isGrabbed;
    }

    public void PastLeverGrabbed(bool isGrabbed)
    {
        pastLeverGrabbed.Value = isGrabbed;
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void FutureMoveServerRpc(Vector3 vec, Quaternion rot) 
    {
        futureRot.Value = rot;
        rotation.Value += vec;
    }



    public void SolveGlobe()
    {
        globeState.Value = GlobeStates.Solved;
    }

    private void Update()
    {

        //Debug.Log(Quaternion.Angle(Quaternion.Euler(targetRot.Value), Quaternion.Euler(rotation.Value)));
        if (true)
        {
            //targetHoriz.Value += new Vector3(0, 0.1f, 0);
            float prog = Mathf.Sin(Time.timeSinceLevelLoad) / 2 + 0.5f;
            float vertRot = Mathf.Lerp(0, 70, prog);
            targetRot.Value = new Vector3(0, targetRot.Value.y + 0.1f, vertRot);
            //vert.Rotate(0, 0, prog);
        }      
    }


}
