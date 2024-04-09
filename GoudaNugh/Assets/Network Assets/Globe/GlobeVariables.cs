using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GlobeVariables : Variables
{
    public NetworkVariable<Vector3> pastRot = new NetworkVariable<Vector3>();
    public NetworkVariable<Vector3> futureRot = new NetworkVariable<Vector3>();


    public void PastMove(Vector3 vec)
    {
        pastRot.Value = vec;
    }

    [Rpc(SendTo.Server)]
    public void FutureMoveServerRpc(Vector3 vec) 
    { 
        futureRot.Value = vec;
    }


}
