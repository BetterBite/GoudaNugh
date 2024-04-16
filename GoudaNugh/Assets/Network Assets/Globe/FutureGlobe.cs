using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutureGlobe : FutureObject
{
    private GlobeVariables vars;
    public CounterMove counter;
    public Transform pastAxis;
    public override void Setup()
    {
        vars = networkObject.GetComponent<GlobeVariables>();
        vars.rotation.OnValueChanged += UpdateRotation;
    }

    public void FutureMove(Vector3 vec)
    {
        vars.FutureMoveServerRpc(vec);
    }

    private void UpdateRotation(Vector3 prevVec, Vector3 currVec) 
    {
        counter.transform.rotation = Quaternion.Euler(currVec);
    }

}
