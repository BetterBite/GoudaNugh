using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutureGlobe : FutureObject
{
    private GlobeVariables vars;
    public CounterMove counter;
    public override void Setup()
    {
        vars = networkObject.GetComponent<GlobeVariables>();
        vars.pastRot.OnValueChanged += ReceivePastMove;
        vars.futureRot.OnValueChanged += ReceiveFutureMove;
    }

    public void FutureMove(Vector3 vec)
    {
        vars.FutureMoveServerRpc(vec);
    }

    private void ReceiveFutureMove(Vector3 prevVec, Vector3 currVec)
    {
        counter.rotate(currVec);
    }

    private void ReceivePastMove(Vector3 prevVec, Vector3 currVec) 
    {
        counter.rotate(currVec);
    }

}
