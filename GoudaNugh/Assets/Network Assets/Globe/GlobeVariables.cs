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

    public void PastMove(Vector3 vec)
    {
        rotation.Value += vec;
    }

    

    [Rpc(SendTo.Server)]
    public void FutureMoveServerRpc(Vector3 vec) 
    {
        rotation.Value += vec;
    }

    public void GlobeOn()
    {
        globeOn.Value = true;
    }

    private void Update()
    {

        if (globeOn.Value)
        {
            //targetHoriz.Value += new Vector3(0, 0.1f, 0);
            float prog = Mathf.Sin(Time.timeSinceLevelLoad) / 2 + 0.5f;
            float vertRot = Mathf.Lerp(0, 90, prog);
            targetRot.Value = new Vector3(0, targetRot.Value.y + 0.1f, vertRot);
            //vert.Rotate(0, 0, prog);
        }      
    }


}
