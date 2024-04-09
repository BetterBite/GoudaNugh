using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PastGlobe : PastObject
{
    private GlobeVariables vars;
    public CounterMove counter;
    public override void Setup()
    {
        vars = networkObject.GetComponent<GlobeVariables>();
        vars.futureRot.OnValueChanged += ReceiveFutureMove;
        vars.pastRot.OnValueChanged += ReceivePastMove;
    }

    public void PastMove(Vector3 vector)
    {
        vars.PastMove(vector);
    }

    public void ReceivePastMove(Vector3 prevVec, Vector3 currVec) 
    { 
        counter.rotate(currVec);
    }

    public void ReceiveFutureMove(Vector3 prevVec, Vector3 currVec)
    {
        counter.rotate(currVec);
    }

    
}
