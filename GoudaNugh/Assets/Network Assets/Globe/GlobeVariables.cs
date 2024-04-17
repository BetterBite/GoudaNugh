using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GlobeVariables : Variables
{
    public NetworkVariable<Vector3> rotation = new NetworkVariable<Vector3>();
    public NetworkVariable<Quaternion> targetHoriz = new NetworkVariable<Quaternion>();
    public NetworkVariable<Quaternion> targetVert = new NetworkVariable<Quaternion>();   

    public void PastMove(Vector3 vec)
    {
        rotation.Value += vec;
    }

    [Rpc(SendTo.Server)]
    public void FutureMoveServerRpc(Vector3 vec) 
    {
        rotation.Value += vec;
    }

    private void Update()
    {
        targetHoriz.Value = Quaternion.Euler(0, targetHoriz.Value.y + 0.1f, 0);
        float prog = Mathf.Sin(Time.timeSinceLevelLoad)/2 + 0.5f;
        float vertRot = Mathf.Lerp(0, 90, prog);
        targetVert.Value = Quaternion.Euler(0, 0, vertRot);
        //vert.Rotate(0, 0, prog);

    }


}
